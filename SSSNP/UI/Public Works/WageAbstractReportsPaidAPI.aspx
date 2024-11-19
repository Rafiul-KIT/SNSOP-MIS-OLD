<%@ Page Title=":: :: SNSOP : : Wages Report from Pay Connect Team" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="WageAbstractReportsPaidAPI" CodeBehind="WageAbstractReportsPaidAPI.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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
            printWindow.document.write('<html><head><title>Wage Paid Requests Abstract with Invoice</title>');
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

    <script type="text/javascript">
        function CheckAll(id) {
            var masterTable = $find("<%= rgDetails.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("chkSelected").checked = true; // for checking the checkboxes
                }
            }
            else {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("chkSelected").checked = false; // for unchecking the checkboxes
                }
            }
        }
        function unCheckHeader(id) {
            var masterTable = $find("<%= rgDetails.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
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
            <div align="left" style="vertical-align: top">
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
                        <i class="fa fa-edit"></i><a href="#">Wage Paid Requests Abstract with Invoice</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Wage Paid Requests Abstract with Invoice</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
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
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
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
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
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
                                                <tr runat="server">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Work Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtWorkcode" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
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
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="BtnSave3" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="BtnSave3_Click" TabIndex="10" Text="Search"
                                                ValidationGroup="group" Width="110px" />
                                            &nbsp;<asp:Button ID="BtnSave4" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Cancel"
                                                ValidationGroup="group" Width="110px" />
                                            &nbsp;<asp:Button ID="btnexp" runat="server" CausesValidation="False"
                                                CssClass="btn btn-primary" Height="32px"
                                                OnClick="btnexp_Click" Style="position: static" Text="Export"
                                                ValidationGroup="vg" Width="110px" />
                                            &nbsp;<asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnSave_Click" OnClientClick="return PrintPanel();" ToolTip="Print Invoices List" TabIndex="10" Text="Print List"
                                                ValidationGroup="group" Width="110px" />
                                            &nbsp;<asp:Button ID="btnexp0" runat="server" CausesValidation="False"
                                                CssClass="btn btn-primary" Height="32px"
                                                OnClick="btnexp0_Click" Style="position: static" Text="Export All"
                                                ValidationGroup="vg" Width="110px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="btnPrintSelected" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="btnPrintSelected_Click"
                                                Text="Print Selected Invoices"
                                                ValidationGroup="group" Width="200px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px; height: 117px;" valign="top">
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
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadGrid ID="rgDetails" PageSize="250" AutoGenerateColumns="false" runat="server" AllowPaging="false"
                                                                    AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both"
                                                                    MasterTableView-TableLayout="Fixed" tablelayout="Auto"
                                                                    OnNeedDataSource="rgDetails_NeedDataSource" OnItemDataBound="rgDetails_ItemDataBound">
                                                                    <HeaderStyle CssClass="MyGridHeader" />
                                                                    <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                                    <ClientSettings Selecting-AllowRowSelect="true">
                                                                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" AllowResizeToFit="true" />
                                                                        <Selecting AllowRowSelect="True" />
                                                                        <%--<ClientEvents OnRowSelected="rowSelected" />--%>
                                                                    </ClientSettings>
                                                                    <MasterTableView TableLayout="Fixed" HeaderStyle-CssClass="MyGridHeader"
                                                                        DataKeyNames="WorkCode,intWagePaymentReqid"
                                                                        ClientDataKeyNames="WorkCode,intWagePaymentReqid">
                                                                        <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                        <HeaderStyle CssClass="MyGridHeader" />
                                                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn UniqueName="checkColumn">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAll(this)" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelected" runat="server" onclick="unCheckHeader(this)" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="20px" />
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="rowNumberLabel" runat="server" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="3%" />
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn UniqueName="tcInvoice" HeaderText="View Invoice Details">
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="hlInvoice" runat="server" Text="Invoice" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="5%" />
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn DataField="IPName" HeaderText="IP Name" HeaderStyle-Width="8%"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="StateName" HeaderText="State" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="CountieName" HeaderText="County" HeaderStyle-Width="6%"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="PayamName" HeaderText="Payam" HeaderStyle-Width="7%"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma" HeaderStyle-Width="14%"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="WorkCode" HeaderText="Work Code" HeaderStyle-Width="13%"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="ReqNo" HeaderText="Request Number" HeaderStyle-Width="12%"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" HeaderStyle-Width="7%"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" HeaderStyle-Width="7%"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="WagePaymentCycle" HeaderText="Payment Cycle" HeaderStyle-Width="9%"></telerik:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
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
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
                <ProgressTemplate>
                    <div class="overlay">
                        <%--<div style=" z-index: 1000; margin-left: 350px;margin-top:200px;opacity: 1;-moz-opacity: 1;">--%>
                        <div style="z-index: 1000; margin-left: -210px; margin-top: 100px; opacity: 1; -moz-opacity: 1;">
                            <img alt="" src="../../Resource/Images/Processing.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexp" />
            <asp:PostBackTrigger ControlID="btnexp0" />
            <asp:PostBackTrigger ControlID="btnPrintSelected" />
        </Triggers>
    </asp:UpdatePanel>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

