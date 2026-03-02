<%@ Page Title=":: :: SNSOP : : Wages Report" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="PDCHouseholdAttendanceRptWagesMultiple" CodeBehind="PDCHouseholdAttendanceRptWagesMultiple.aspx.cs" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script>
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
    <style>
        .res {
            overflow-x: scroll;
        }

        .RadGrid {
            border-width: 0px;
            border-style: solid;
        }

        .MyGridHeader {
            background-color: #dedede !important;
            font-family: Poppins;
            vertical-align: middle;
            /*background-image:none !important;*/
        }

        .MyGridAlternatingRow {
            font-family: Poppins;
            background-color: #D1DDF1 !important;
        }

        .RadGrid_Default .rgRow > td {
            font-family: Poppins;
            background-color: #fff;
        }

        div.RadGrid_Default .rgHeader, div.RadGrid_Default th.rgResizeCol .HeaderTitle {
            background-image: none;
            color: #fff !important;
            font-size: 12px !important;
            font-weight: bold;
            font-family: Poppins;
            background-color: #013161 !important;
            vertical-align: top;
        }

            div.RadGrid_Default .rgHeader a {
                color: #fff !important;
                font-size: 12px !important;
                font-weight: bold;
                font-family: Poppins;
                vertical-align: top;
            }
    </style>
    <script type="text/javascript" language="javascript">

        function OpenPopup() {

            window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

            return false;
        }
    </script>
    <style type="text/css">
        .res {
            overflow-x: scroll;
        }
    </style>

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
                        <i class="fa fa-edit"></i><a href="#">Multiple Wages Report</a>
                    </li>
                </ol>
            </div>

            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Multiple Wages Report of Biometric</h3>
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
                                                            Height="28px" Width="180px">
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
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Work Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtWorkcode" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>



                                                <tr>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;"></td>
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
                                                CssClass="btn btn-primary" Height="32px"
                                                OnClick="btnExportCSV_Click" Style="position: static" Text="Export"
                                                ValidationGroup="vg" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnSave_Click" OnClientClick="return PrintPanel();" ToolTip="Export Excel Sheet" TabIndex="10" Text="Print"
                                                ValidationGroup="group" Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <div runat="server" id="div_print">
                                                <asp:Panel ID="pnlContents" runat="server">

                                                    <table runat="server" id="tblreport" visible="false" class="table table-bordered table-responsive" bordercolor="#003399" borderstyle="Solid" borderwidth="1px">
                                                        <tr style="overflow-x: scroll; overflow-y: hidden;">
                                                            <td colspan="3" style="max-width: 750px">
                                                                <telerik:RadGrid class="RadGrid" ID="rgWagesReport" PagerStyle-Font-Size="12px" PageSize="50" AutoGenerateColumns="true" runat="server" AllowPaging="true"
                                                                    AllowMultiRowSelection="true" AllowSorting="true" GridLines="None" GridLinesVisibility="None"
                                                                    Font-Names="Poppins" ForeColor="#333333"
                                                                    OnNeedDataSource="rgWagesReport_NeedDataSource" OnItemDataBound="rgWagesReport_ItemDataBound" OnItemCommand="rgWagesReport_ItemCommand">
                                                                    <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                                    <MasterTableView TableLayout="Auto" Width="100%"
                                                                        DataKeyNames=""
                                                                        ClientDataKeyNames="">
                                                                        <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="rowNumberLabel" runat="server" Style="border-top-left-radius: 10px;" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="rgHeader" BackColor="#013161" />
                                                                            </telerik:GridTemplateColumn>

                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblResultA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#CC3300"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </div>
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
