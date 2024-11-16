<%@ Page Title=":: :: SNSOP : : Wages Report from Pay Connect Team" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="WageInvoiceAbstractAPIPaid_DIS" CodeBehind="WageInvoiceAbstractAPIPaid_DIS.aspx.cs" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);
            $('#txtStartDate').datetimepicker();
        });
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

    <script type="text/javascript" language="javascript">
        function OpenPopup() {
            window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");
            return false;
        }
    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                            </li>
                    <li class="">
                        <i class="fa fa-fw fa-desktop"></i>
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Paid Wage Request Abstract </a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Paid Wage Request Abstract DIS</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label443" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label373" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label404" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="28px"
                                                            OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox"
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label412" runat="server" CssClass="LBLBLACK" Width="128px">From Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:TextBox ID="TxtFromDate" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                                            PopupButtonID="TxtFromDate" TargetControlID="TxtFromDate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                 <%--       <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TxtFromDate"
                                                            ErrorMessage="Please Enter From Date" InitialValue="" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="128px">To Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:TextBox ID="TxtToDate" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                                            PopupButtonID="TxtToDate" TargetControlID="TxtToDate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                    <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtToDate"
                                                            ErrorMessage="Please Enter To Date" InitialValue="" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox"
                                                            Height="33px" ValidationGroup="group" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr id="Tr1" runat="server">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">Request No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtRequestNo" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr id="Tr2" runat="server">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblPaymentCycle" runat="server" CssClass="LBLBLACK" Width="144px">Payment Cycle</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPaymentCycle" runat="server" class="form-control txtbox"
                                                            Height="33px" ValidationGroup="group" Width="180px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="BtnSave3" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="BtnSave3_Click" TabIndex="10" Text="Search"
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnSave4" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Cancel"
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="btnexp" runat="server" CausesValidation="False"
                                                CssClass="btn btn-primary" Font-Size="12px" Height="32px"
                                                OnClick="btnexp_Click" Style="position: static" Text="Export"
                                                ValidationGroup="vg" Visible="false" />
                                            &nbsp;<asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnSave_Click" OnClientClick="return PrintPanel();" ToolTip="Export Excel Sheet" TabIndex="10" Text="Print"
                                                ValidationGroup="group" Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <div runat="server" id="div_print">
                                                <asp:Panel ID="pnlContents" runat="server">
                                                    <table runat="server" id="tblreport" visible="false" style="padding: 5px">
                                                        <tr>
                                                            <td>

                                                                <asp:Label ID="LblResultA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#CC3300"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="LblResultA0" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#CC3300"></asp:Label>
                                                                <br />
                                                                <asp:GridView ID="grdClosebatchdet" runat="server" CellPadding="4"
                                                                    CssClass="GRD" ForeColor="#333333"
                                                                    OnPageIndexChanging="grdClosebatchdet_PageIndexChanging" AutoGenerateColumns="false"
                                                                    OnRowDataBound="grdClosebatchdet_RowDataBound"
                                                                    OnSelectedIndexChanged="grdClosebatchdet_SelectedIndexChanged" PageSize="50"
                                                                    Width="100%">
                                                                    <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                                    <RowStyle BackColor="#FFFFFF" CssClass="GRDITEM" HorizontalAlign="Left"
                                                                        VerticalAlign="Middle" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex +1 %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="30px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Beneficiaries Selected for Payment">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink runat="server" class="fa fa-edit" Target="_blank" ID="hlRedirect" NavigateUrl='<%# String.Format("ViewBenfWages.aspx?WorkReqId={0}",  Eval("intWagePaymentReqid")) %>' Text='View'>
                                                                                </asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="View Invoice Details">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink runat="server" class="fa fa-edit" Target="_blank" ID="hlRedirectAABC" NavigateUrl='<%# String.Format("ViewWagesReqDetailsAutoRec.aspx?WorkReqId={0}",  Eval("intWagePaymentReqid")) %>' Text='Invoice'>
                                                                                </asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="IPName" HeaderText="IP Name" />
                                                                        <asp:BoundField DataField="StateName" HeaderText="State" />
                                                                        <asp:BoundField DataField="CountieName" HeaderText="County" />
                                                                        <asp:BoundField DataField="PayamName" HeaderText="Payam" />
                                                                        <asp:BoundField DataField="BomaName" HeaderText="Boma" />
                                                                        <asp:BoundField DataField="WorkCode" HeaderText="Work Code" />
                                                                        <asp:BoundField DataField="RequestNo" HeaderText="Request Number" />
                                                                        <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                                                        <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                                                        <asp:BoundField DataField="TotalMembers" HeaderText="Total Members" />
                                                                        <asp:BoundField DataField="TotalWorkDays" HeaderText="Work Days" />
                                                                        <asp:BoundField DataField="TotalAmount" HeaderText="Amount in USD" />
                                                                        <asp:BoundField DataField="TotalMembersPaid" HeaderText="Paid Members" />
                                                                        <asp:BoundField DataField="AmountPaidUSD" HeaderText="Paid Amount (USD)" />
                                                                        <asp:BoundField DataField="AmountPaidSSP" HeaderText="Paid Amount (SSP)" />
                                                                        <asp:BoundField DataField="WageStatus" HeaderText="Wage Status" />
                                                                        <asp:BoundField DataField="PaymentCycle" HeaderText="Payment Cycle" />
                                                                    </Columns>
                                                                    <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                                    <EmptyDataTemplate>
                                                                        There is no attendance for this work in&nbsp; these days
                                                                    </EmptyDataTemplate>
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True"
                                                                        ForeColor="White" />
                                                                    <EditRowStyle BackColor="#013161" />
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="lblmessage" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#CC3300"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexp" />
        </Triggers>
    </asp:UpdatePanel>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>
