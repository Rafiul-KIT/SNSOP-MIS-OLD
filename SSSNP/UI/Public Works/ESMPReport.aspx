<%@ Page Title="Environment Management Plan Abstract" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="UI_Public_Works_GrievanceRegistrationReport" CodeBehind="ESMPReport.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);
            $('#txtComplaintRegisteredDate').datetimepicker();
        });
    </script>
    <script type="text/javascript" language="javascript">

        function OpenPopup() {
            window.open("Lookups/GrievanceRegistrationLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");
            return false;
        }
    </script>

    <script type="text/javascript">
        function inputOnlyNumbers(evt) {
            var e = window.event || evt; // for trans-browser compatibility  
            var charCode = e.which || e.keyCode;
            if ((charCode > 45 && charCode < 58) || charCode == 8) {
                return true;
            }
            return false;
        }
    </script>

    <%-- <asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>--%>
    <div align="left">
        <ol class="breadcrumb">
            You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                           
                            <li class="active">
                                <i class="fa fa-edit"></i><a href="#">Environment Management Plan Abstract</a>
                            </li>
        </ol>
    </div>
    <div align="center">
        <div class="row" align="center">
            <div class="col-lg-11">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Environment Management Plan Abstract</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                            <tr>
                                <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 83%"
                                        __designer:mapid="17eb">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="165px">From Date</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">:</td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:TextBox ID="txtFromDate" runat="server" class="form-control txtbox"
                                                    Height="28px" MaxLength="10" TabIndex="1" Width="180px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                    PopupButtonID="txtFromDate" TargetControlID="txtFromDate"></cc1:CalendarExtender>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;"></td>
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
                                    </table>

                                </td>
                                <td style="width: 27px">&nbsp;</td>
                                <td valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%"
                                        __designer:mapid="1800">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="165px">To Date </asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">:</td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:TextBox ID="txtToDate" runat="server" class="form-control txtbox"
                                                    Height="28px" MaxLength="10" TabIndex="1" Width="180px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                    PopupButtonID="txtToDate" TargetControlID="txtToDate"></cc1:CalendarExtender>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg1" runat="server" Font-Bold="True" Font-Names="verdana"
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="padding: 5px; margin: 5px" valign="top">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary"
                                        Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Search"
                                        ValidationGroup="group" Width="90px" />
                                    &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger"
                                        Height="32px" OnClick="BtnClear_Click" TabIndex="10" Text="Cancel"
                                        ValidationGroup="group" Width="90px" />
                                    &nbsp;<asp:Button ID="Button1" runat="server" CssClass="btn btn-default"
                                        Height="32px" OnClick="Button1_Click" TabIndex="10" Text="Export"
                                        ValidationGroup="group" Width="90px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <div id="success" runat="server" class="alert alert-success" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert"
                                            href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label
                                                ID="lblmsg" runat="server"></asp:Label>
                                    </div>

                                    <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                                        <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <div runat="server" id="div_print">
                                        <asp:Panel ID="pnlContents" runat="server">
                                            <asp:GridView ID="grdDetails" runat="server"
                                                AutoGenerateColumns="False" CellPadding="4" CssClass="table table-responsive table-bordered" ForeColor="#333333"
                                                GridLines="None" Height="62px" ShowFooter="true"
                                                OnPageIndexChanging="grdDetails_PageIndexChanging"
                                                OnRowDataBound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left"
                                                    VerticalAlign="Middle" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex +1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="IPName" HeaderText="IP Name" />
                                                    <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                                    <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                                    <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                                    <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                                    <asp:TemplateField HeaderText="No of registered">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("TotalPlans") %>' Target="_blank"></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="TotalPlans" HeaderText="No of registered" Visible="false" />
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
                        <asp:HiddenField ID="hdfID" Value="" runat="server" />
                        <asp:HiddenField ID="hdFromdate" runat="server" />
                        <asp:HiddenField ID="hdToDate" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="group1" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
