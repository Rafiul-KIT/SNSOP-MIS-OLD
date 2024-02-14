<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="UI_EnvironmentManagmentReport"
    Title="Environment Managment Report" CodeBehind="EnvironmentManagmentReport.aspx.cs" %>

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
            You are here &nbsp;!&nbsp; &nbsp; &nbsp;           
            <li class="active"><i class="fa fa-edit"></i><a href="#">Environment Management Plan Report</a> </li>
        </ol>
    </div>
    <div align="center">
        <div class="row" align="center">
            <div class="col-lg-11">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Environment Management Plan Report</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                            <tr>
                                <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 43%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK">Work Code</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">:
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:TextBox ID="txtWorkCode" runat="server" class="form-control txtbox" Height="28px"
                                                    MaxLength="100" TabIndex="1" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;"></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 27px">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg1" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="padding: 5px; margin: 5px" valign="top">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                        OnClick="BtnSave_Click" TabIndex="10" Text="Search" ValidationGroup="group" Width="90px" />
                                    &nbsp;
                                    <asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger" Height="32px"
                                        OnClick="BtnClear_Click" TabIndex="10" Text="Cancel" ValidationGroup="group"
                                        Width="90px" />&nbsp;                                         
                                    <asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-warning" Height="32px"
                                        OnClick="PrintAllPages" ToolTip="Print" TabIndex="10" Text="Print" ValidationGroup="group"
                                        Width="90px" />
                                    <asp:Button ID="btnexp" runat="server" CausesValidation="False" CssClass="btn btn-success"
                                        Font-Size="12px" Height="32px" OnClick="btnexp_Click" Style="position: static"
                                        Text="Export" ValidationGroup="vg" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <div id="success" runat="server" class="alert alert-success" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert" href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
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
                                            <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" CssClass="table table-responsive table-bordered" ForeColor="#333333"
                                                GridLines="None" Height="62px" OnPageIndexChanging="grdDetails_PageIndexChanging"
                                                OnRowDataBound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <Columns>
                                                    <asp:BoundField DataField="NoOfReg" HeaderText="Work Code" />
                                                    <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                                    <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                                    <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                                    <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                                    <%-- <asp:TemplateField HeaderText="Work Code">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("NoOfReg") %>' Target="_blank"></asp:HyperLink>
                                                                <%--NavigateUrl='<%# String.Format( "EnvironmentRegistrationReportDetails.aspx?BomaId={0}&WorkCode={1}", Eval("BomaId"),Eval("WorkCodeID")) %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" CommandArgument="<%NoOfReg%>" class="fa fa-edit"
                                                                runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NoOfReg" HeaderText="No of complaints registered" Visible="false" />--%>
                                                    <asp:BoundField DataField="EnvironmentSocialImpact" HeaderText="Environment Social Impact" />
                                                    <asp:BoundField DataField="ProposedMitigationMeasure" HeaderText="Proposed Mitigation Measure" />
                                                    <asp:BoundField DataField="MitigationMeasureImplemented" HeaderText="Mitigation Measure Implemented" />
                                                    <asp:BoundField DataField="PerformanceIndicators" HeaderText="Performance Indicators" />
                                                    <%--<asp:BoundField DataField="FileName" HeaderText="FileName" />--%>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="File">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FileName") %>' />
                                                            <asp:HyperLink ID="hypScanLetter0" runat="server" CommandName="PathUpdate" CssClass="LBLBLACKFOOTER" Text='<%# Eval("FilePath") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
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
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="group" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="group1" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
