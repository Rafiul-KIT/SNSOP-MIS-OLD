<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="UI_Public_Works_EnvironmentManagmentPlan"
    Title="Environment Management Plan" Codebehind="EnvironmentManagmentPlan.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function OpenPopup() {

            window.open("Lookups/EnviranmnetManagementLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

            return false;
        }

        function ApplyChanges(val, val1) {

            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

            document.forms[0].submit();
        }
    </script>

    <div align="left">
        <ol class="breadcrumb">
            You are here &nbsp;!&nbsp; &nbsp; &nbsp;
            <li><i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a> </li>
            <li class=""><i class="fa fa-fw fa-desktop"></i>PDC </li>
            <li class="active"><i class="fa fa-edit"></i><a href="#">Environment Management</a>
            </li>
        </ol>
    </div>
    <div align="center">
        <div class="row" align="center">
            <div class="col-lg-11">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Environment Management Plan</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                    <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="165px">Search</asp:Label>
                                    <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px"
                                        Text="Look Up" CausesValidation="False" Font-Size="12px" Style="position: static"
                                        ToolTip="Rate Lookup" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                    <asp:Label ID="lblGrievance_RegistrationNo" Font-Bold="true" Visible="false" runat="server"
                                        CssClass="LBLBLACK" Width="365px"></asp:Label>
                                </td>
                                <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="144px">IP</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 224px;">
                                                <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox" Height="33px"
                                                    Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlIP_SelectedIndexChanged">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">Project Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 224px;">
                                                <asp:DropDownList ID="ddlProject" runat="server" class="form-control txtbox" Height="33px"
                                                    Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="rvddlProject" runat="server" ControlToValidate="ddlProject"
                                                    ErrorMessage="Please Select Name Of SubProject" InitialValue="--Select--" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label88" runat="server" CssClass="LBLBLACK" Width="144px">State</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 224px;">
                                                <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox" Height="33px"
                                                    Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="rvddlState" runat="server" ControlToValidate="ddlState"
                                                    ErrorMessage="Please Select State" InitialValue="--Select--" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="144px">County / Municipality Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 224px;">
                                                <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox" Height="33px"
                                                    Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCounties"
                                                    ErrorMessage="Please Select County" InitialValue="--Select--" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 27px">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="144px">Payam / Block Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" Height="28px"
                                                    Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlPayam"
                                                    ErrorMessage="Please Select Payam" InitialValue="--Select--" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Width="144px">Boma / Quarter Council Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" Height="28px"
                                                    Width="180px">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlBoma"
                                                    ErrorMessage="Please Select Boma" InitialValue="--Select--" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label12" runat="server" CssClass="LBLBLACK" Width="144px">Work code</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="lblWorkCode" runat="server" Height="28px" TabIndex="1" Width="180px" />
                                            </td>
                                            <%--<td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="rvtxtWorkCode" runat="server" ControlToValidate="txtWorkCode"
                                                    ErrorMessage="Please Enter Work Code" InitialValue="" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="144px">File Upload (PDF,DOC and JPG Only)</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:FileUpload ID="FileUpload1" runat="server" onchange="this.form.submit();" class="form-control txtbox"
                                                    Height="28px" />
                                                <asp:Label ID="lblFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                 <asp:HyperLink ID="HyperLink1" Target="_blank" runat="server" CssClass="LBLBLACKFOOTER">[hypScanLetter]</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Environment Social  Impact</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtEnvironmentImpact" runat="server" class="form-control txtbox"
                                                    Height="38px" MaxLength="200" oncopy="return false" oncut="return false" onpaste="return false"
                                                    TabIndex="1" ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="rvtxtEnvironmentImpact" runat="server" ControlToValidate="txtEnvironmentImpact"
                                                    ErrorMessage="Please Enter Environment Impact" InitialValue="" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label11" runat="server" CssClass="LBLBLACK" Width="144px">Proposed Mitigation Measure</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtProposedMitigationMeasure" runat="server" class="form-control txtbox"
                                                    Height="38px" MaxLength="200" oncopy="return false" oncut="return false" onpaste="return false"
                                                    TabIndex="1" ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="rvtxtProposedMitigationMeasure" runat="server" ControlToValidate="txtProposedMitigationMeasure"
                                                    ErrorMessage="Please Enter Proposed Mitigation Measure" InitialValue="" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 27px">
                                    &nbsp;
                                </td>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="144px">Mitigation Measure Implemented</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtMitigationMeasure" runat="server" class="form-control txtbox"
                                                    Height="38px" MaxLength="200" oncopy="return false" oncut="return false" onpaste="return false"
                                                    TabIndex="1" ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="rvtxtMitigationMeasure" runat="server" ControlToValidate="txtMitigationMeasure"
                                                    ErrorMessage="Please Enter Mitigation Measure" InitialValue="" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="144px">Performance Indicators</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px;">
                                                <asp:TextBox ID="txtPerformanceIndicator" runat="server" class="form-control txtbox"
                                                    Height="38px" MaxLength="200" oncopy="return false" oncut="return false" onpaste="return false"
                                                    TabIndex="1" ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="rvtxtPerformanceIndicator" runat="server" ControlToValidate="txtPerformanceIndicator"
                                                    ErrorMessage="Please Enter Performance Indicator" InitialValue="" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                        TabIndex="10" Text="Submit" ValidationGroup="group" Width="90px" OnClick="BtnSave1_Click" />
                                    &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger" Height="32px"
                                        TabIndex="10" Text="Cancel" Width="90px" OnClick="BtnSave2_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <div id="success" runat="server" class="alert alert-success" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert" href="AddQualification.aspx">
                                            ×</a> <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
                                    </div>
                                    <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                                        <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                            <td style="color:Red">
                                <asp:Label ID="lblResult" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdfID" Value="" runat="server" />
                        <asp:HiddenField ID="hdfUploadFile1" runat="server" />
                        <asp:HiddenField ID="hdfFlagID" runat="server" />
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
