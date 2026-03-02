<%@ Page Title="Add wage rate" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="WageRate" CodeBehind="WageRate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

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

    <style type="text/css">
        .overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 112px;
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.9;
            -moz-opacity: 0.9;
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
                        <i class="fa fa-fw fa-edit"></i>TST Team
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Wage Rate</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Add Wage Rate</h3>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="panel-body">
                                        <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK">State</asp:Label></td>
                                                            <td style="padding: 5px; margin: 5px">:</td>

                                                            <td style="padding: 5px; margin: 5px">&nbsp;
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox"
                                                            Height="33px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                            AutoPostBack="True">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                    ControlToValidate="ddlState" ErrorMessage="Please Select State"
                                                                    ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="165px">County/Municipality Name</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox"
                                                                    Height="33px">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                    ControlToValidate="ddlCounties" ErrorMessage="Please Select County"
                                                                    ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK">Wage Rate 
                                                        in USD per beneficiary</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:TextBox ID="txt_wage_rate" runat="server" class="form-control txtbox"
                                                                    Height="35px" onkeypress="return inputOnlyNumbers(event);" TabIndex="1"
                                                                    ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="txt_wage_rate" ErrorMessage="Please Enter Wage Rate"
                                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary"
                                                        Height="32px" TabIndex="10" Text="Save"
                                                        ValidationGroup="group" Width="90px" OnClick="BtnSave1_Click" />
                                                    &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False"
                                                        CssClass="btn btn-danger" Height="32px" TabIndex="10" OnClick="BtnClear_Click"
                                                        Text="Clear" ToolTip="To Clear  the Screen" Width="90px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                    <div id="success" runat="server" visible="false" class="alert alert-success">
                                                        <a href="AddQualification.aspx" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                                        <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
                                                    </div>
                                                    <div id="Failure" runat="server" visible="false" class="alert alert-danger">
                                                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                                        <strong>Warning!</strong>
                                                        <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hdfID" runat="server" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                        <asp:GridView ID="GD_List" runat="server"
                                            AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" ForeColor="#333333"
                                            GridLines="None" Width="90%" OnRowCommand="GD_List_RowCommand" >
                                            <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left"
                                                VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnl_select" runat="server" Text="Select" CommandArgument='<%# Eval("intExachageid")%>'
                                                            CommandName="select" CausesValidation="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="intStateid" HeaderText="State Code" />
                                                <asp:BoundField DataField="StateName" HeaderText="State" />
                                                <asp:BoundField DataField="intCountyid" HeaderText="County Code" />
                                                <asp:BoundField DataField="CountieName" HeaderText="County" />
                                                <asp:BoundField DataField="WageRate" HeaderText="Wage Rate" />
                                            </Columns>
                                            <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
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
    </asp:UpdatePanel>
</asp:Content>
