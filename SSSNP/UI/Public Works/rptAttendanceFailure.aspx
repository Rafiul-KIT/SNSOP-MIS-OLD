<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="rptAttendanceFailure"
    Title=":: SSSNP Online Management System ::" Codebehind="rptAttendanceFailure.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
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

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future date!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>

    <div class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title" id="h3" runat="server">
                Attendance Failure Report</h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-6 col-xs-12">
                    <div class="row" runat="server" id="Div4" visible="true">
                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                            <asp:Label class="control-label" ID="Label4" runat="server">work id (Last Four Digits)</asp:Label>
                        </div>
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                            <asp:TextBox ID="txtWorkID" runat="server" class="form-control"
                                autocomplete="off" ValidationGroup="group"></asp:TextBox>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        </div>
                    </div>
                    <div class="row" runat="server" id="Div2" visible="true">
                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                            <asp:Label class="control-label" ID="Label2" runat="server">Household Number</asp:Label>
                        </div>
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                            <asp:TextBox ID="txthouseholdNo" runat="server" class="form-control" TextMode="MultiLine"
                                autocomplete="off" ValidationGroup="group"></asp:TextBox>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-xs-12">
                    <div class="row" runat="server" id="Div1" visible="true">
                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                            <asp:Label class="control-label" ID="Label1" runat="server">Date</asp:Label>
                        </div>
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                            <asp:TextBox ID="txtDate" runat="server" class="form-control" MaxLength="15" autocomplete="off"
                                ValidationGroup="group"></asp:TextBox>
                            <cc1:CalendarExtender OnClientDateSelectionChanged="checkDate" ID="CalendarExtender2"
                                runat="server" Format="dd-MM-yyyy" TargetControlID="txtDate">
                            </cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        </div>
                    </div>
                    <div class="row" runat="server" id="Div3" visible="true">
                        <div class="col-md-5 col-sm-12 col-xs-12 form-group">
                            <asp:Label class="control-label" ID="Label3" runat="server">Attendance Date</asp:Label>
                        </div>
                        <div class="col-md-6 col-sm-12 col-xs-12 form-group">
                            <asp:TextBox ID="txtADate" runat="server" class="form-control" MaxLength="15" autocomplete="off"
                                ValidationGroup="group"></asp:TextBox>
                            <cc1:CalendarExtender OnClientDateSelectionChanged="checkDate" ID="CalendarExtender1"
                                runat="server" Format="dd-MM-yyyy" TargetControlID="txtADate">
                            </cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1 col-sm-12 col-xs-12 form-group">
                        </div>
                    </div>
                </div>
            </div>
            <div id="Div5" class="row" runat="server" visible="true">
                <div class="col-md-1 col-md-offset-5">
                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" OnClick="BtnSave_Click"
                        TabIndex="10" Text="Search" />
                </div>
                <div class="col-md-1 col-md-offset-0">
                    <asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-danger"
                        OnClick="BtnClear_Click" TabIndex="10" Text="Cancel" ToolTip="To Clear  the Screen" />
                </div>
                <div class="col-md-1 col-md-offset-0">
                    <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="btn btn-info"
                        OnClick="btnExport_Click" TabIndex="10" Text="Export" ToolTip="Click to Export" />
                </div>
            </div>
            <div id="Div6" class="row" runat="server" visible="true">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                    ForeColor="#006600"></asp:Label>
                <asp:Label ID="lblresult" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                    ForeColor="#006600"></asp:Label>
            </div>
            <div id="Div7" class="row" runat="server" visible="true">
                <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" CssClass="GRD table-bordered " ForeColor="#333333" GridLines="None"
                    ShowFooter="true" Height="62px" OnPageIndexChanging="grdDetails_PageIndexChanging"
                    OnRowDataBound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex +1 %>
                                <asp:HiddenField ID="hdfid" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Household_Number" HeaderText="Household Number"></asp:BoundField>
                        <asp:BoundField DataField="work_id" HeaderText="work id"></asp:BoundField>
                        <asp:BoundField DataField="AttedanceDate" HeaderText="Attedance Date"></asp:BoundField> 
                        <asp:BoundField DataField="PullDate" HeaderText="Pull Date"></asp:BoundField>
                        <asp:BoundField DataField="Failure_Message" HeaderText="Failure Message"></asp:BoundField>
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
            </div>
        </div>
    </div>
</asp:Content>
