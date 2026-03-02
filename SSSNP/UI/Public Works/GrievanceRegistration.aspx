<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"
    AutoEventWireup="true" Inherits="GrievanceRegistration" Codebehind="GrievanceRegistration.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script>
        $(document).ready(function() {

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

        function ApplyChanges(val, val1) {

            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

            document.forms[0].submit();
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
            <li><i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a> </li>
            <li class=""><i class="fa fa-fw fa-desktop"></i>PDC </li>
            <li class="active"><i class="fa fa-edit"></i><a href="#">Grievance Registration</a>
            </li>
        </ol>
    </div>
    <div align="center">
        <div class="row" align="center">
            <div class="col-lg-11">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Grievance Registration</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                    <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="165px">Search</asp:Label>
                                    <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px"
                                        OnClick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False" Font-Size="12px"
                                        Style="position: static" ToolTip="Rate Lookup" />
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
                                                <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">State Name</asp:Label>
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlState"
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
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                    <asp:Label ID="Label379" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="233px">Complainant Details:</asp:Label>
                                </td>
                                <td style="width: 27px">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtComplainant_Name" runat="server" class="form-control txtbox"
                                                    Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComplainant_Name"
                                                    ErrorMessage="Please Enter Complainant Name" InitialValue="" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="144px">Enrolment No</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtComplainant_EnrolmentNo" runat="server" class="form-control txtbox"
                                                    Height="28px" MaxLength="25" TabIndex="1" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="144px">Date of Complaint Registered</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px;">
                                                <asp:TextBox ID="txtComplaintRegisteredDate" runat="server" class="form-control txtbox"
                                                    Height="28px" MaxLength="10" TabIndex="1" Width="180px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" PopupButtonID="txtComplaintRegisteredDate"
                                                    TargetControlID="txtComplaintRegisteredDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtComplaintRegisteredDate"
                                                    ErrorMessage="Please Select Date of Complaint Registered" SetFocusOnError="True"
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
                                                <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="144px">Type of Complaints</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtTypeOfComplaints" runat="server" class="form-control txtbox"
                                                    Height="28px" MaxLength="2000" TabIndex="1" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTypeOfComplaints"
                                                    ErrorMessage="Please Enter Type of Complainant" InitialValue="" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="144px">Details of Complaints reported</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtDetailsComplainsReported" runat="server" class="form-control txtbox"
                                                    Height="60px" MaxLength="2000" TabIndex="1" TextMode="MultiLine" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                    <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="244px">Details of the accused:</asp:Label>
                                </td>
                                <td style="width: 27px">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="144px">Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtAccused_Name" runat="server" class="form-control txtbox" Height="28px"
                                                    MaxLength="400" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAccused_Name"
                                                    ErrorMessage="Please Enter Accused Name" InitialValue="" SetFocusOnError="True"
                                                    ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Width="144px">Enrolment No</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtAccused_EnrolmentNo" runat="server" class="form-control txtbox"
                                                    Height="28px" MaxLength="25" TabIndex="1" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
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
                                                <asp:Label ID="Label8" runat="server" CssClass="LBLBLACK" Width="144px">Specify how the complaint was handled</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtAccused_Complaints_Specification" runat="server" class="form-control txtbox"
                                                    Height="28px" MaxLength="2000" TabIndex="1" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK" Width="144px">If the Complaint not handled and referred to whom</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtAccused_ComplainsReferred" runat="server" class="form-control txtbox"
                                                    Height="28px" MaxLength="2000" TabIndex="1" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                    <asp:Label ID="Label11" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="244px">Grievance Addressed Details :</asp:Label>
                                </td>
                                <td style="width: 27px">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label12" runat="server" CssClass="LBLBLACK" Width="144px">Upload Document</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" Width="180px"
                                                    onchange="this.form.submit();" />
                                                <asp:HyperLink ID="hypDocument" runat="server">No Document Selected</asp:HyperLink>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label13" runat="server" CssClass="LBLBLACK" Width="144px">Date of Complaint Addressed</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtAddressedDate" runat="server" class="form-control txtbox" Height="28px"
                                                    MaxLength="10" TabIndex="1" Width="180px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" PopupButtonID="txtAddressedDate"
                                                    TargetControlID="txtAddressedDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
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
                                                <asp:Label ID="Label15" runat="server" CssClass="LBLBLACK" Width="144px">How it was addressed</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtAddressedHow" runat="server" class="form-control txtbox" Height="60px"
                                                    MaxLength="2000" TabIndex="1" TextMode="MultiLine" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
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
                                        OnClick="BtnSave_Click" TabIndex="10" Text="Submit" ValidationGroup="group" Width="90px" />
                                    &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger" Height="32px"
                                        OnClick="BtnClear0_Click" TabIndex="10" Text="Cancel" Width="90px" />
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
    <%--</ContentTemplate>
  </asp:UpdatePanel>--%>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>
