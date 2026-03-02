<%@ Page Title=":: :: SNSOP : : Implentation Partner User Creation Details" Language="C#"
    MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true"
    Inherits="IPUserCreation" Codebehind="IPUserCreation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <style type="text/css">
        .overlay
        {
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

    <script type="text/javascript" language="javascript">



function OpenPopup() 
 
   {

       window.open("Lookups/IPUserCreationLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

    return false;
        }

        function ApplyChanges(val, val1) {

            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

            document.forms[0].submit();
        }
    </script>

    <script type="text/javascript">
    function showProgress() {
        var updateProgress = $get("<%= UpdateProgress.ClientID %>");
        updateProgress.style.display = "block";
    }
    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here &nbsp;!&nbsp; &nbsp; &nbsp;
                    <li><i class="fa fa-dashboard"></i><a href="Home.aspx">Masters</a> </li>
                    <li class="active"><i class="fa fa-edit"></i><a href="#">Implementation Partners Staff
                        Creation</a> </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Implementation Partners Staff Creation</h3>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="panel-body">
                                        <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                                    <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="165px">Search Staff</asp:Label>
                                                    <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px"
                                                        OnClick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False" Font-Size="12px"
                                                        Style="position: static" ToolTip="Rate Lookup" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Name 
                                                        of The IP</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                :
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox" Height="33px"
                                                                    ValidationGroup="group" Width="180px">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIP"
                                                                    ErrorMessage="Please Enter IP Name" InitialValue="--Select--" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="165px">Staff Name</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                :
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:TextBox ID="txtOfficerName" runat="server" class="form-control txtbox" Height="42px"
                                                                    MaxLength="40" TabIndex="1" TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOfficerName"
                                                                    ErrorMessage="Please Enter Staff Name" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="137px">Email Id</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                :
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:TextBox ID="txtmail" runat="server" class="form-control txtbox" Height="34px"
                                                                    MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtmail"
                                                                    ErrorMessage="Please Enter Email" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtmail"
                                                                    ErrorMessage="Please Enter Correct Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ValidationGroup="group">*</asp:RegularExpressionValidator>
                                                                &nbsp;
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
                                                                <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">User 
                                                        Id</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                :
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:TextBox ID="txtuser" runat="server" class="form-control txtbox" Height="28px"
                                                                    MaxLength="10" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtuser"
                                                                    ErrorMessage="Please Enter User ID" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trPass">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label357" runat="server" CssClass="LBLBLACK" Width="137px">Password</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                :
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:TextBox ID="txtpass" runat="server" class="form-control txtbox" Height="28px"
                                                                    MaxLength="10" TabIndex="1" ValidationGroup="group" Width="180px" TextMode="Password"></asp:TextBox>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtpass"
                                                                    ErrorMessage="Please Enter Password" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trPass1">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="137px">Confirm Password</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                :
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:TextBox ID="txtpassconf" runat="server" class="form-control txtbox" Height="28px"
                                                                    MaxLength="10" TabIndex="1" ValidationGroup="group" Width="180px" TextMode="Password"></asp:TextBox>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtpassconf"
                                                                    ErrorMessage="Please Enter Confirm Password" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>&nbsp;
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label365" runat="server" CssClass="LBLBLACK" Width="137px">
                                                        Active/InActive</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                :
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:DropDownList ID="DdlActive" runat="server" class="form-control txtbox" Height="33px"
                                                                    ValidationGroup="group" Width="180px">
                                                                    <asp:ListItem Selected="True" Value="Y">Active</asp:ListItem>
                                                                    <asp:ListItem Value="N">InActive</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="137px">
                                                        Role</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                :
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:DropDownList ID="DdlRole" runat="server" class="form-control txtbox" Height="33px"
                                                                    ValidationGroup="group" Width="180px">
                                                                    <asp:ListItem Selected="True">IP</asp:ListItem>
                                                                    <asp:ListItem>Registration</asp:ListItem>
                                                                    <asp:ListItem>Allocation</asp:ListItem>
                                                                    <asp:ListItem>Attendance</asp:ListItem>
                                                                    <asp:ListItem>Request</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                                        OnClick="BtnSave_Click" TabIndex="10" Text="Save" ValidationGroup="group" Width="90px" />
                                                    &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-danger"
                                                        Height="32px" OnClick="BtnClear_Click" TabIndex="10" Text="ClearAll" ToolTip="To Clear  the Screen"
                                                        Width="90px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                    <div id="success" runat="server" visible="false" class="alert alert-success">
                                                        <a href="AddQualification.aspx" class="close" data-dismiss="alert" aria-label="close">
                                                            &times;</a> <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
                                                    </div>
                                                    <div id="Failure" runat="server" visible="false" class="alert alert-danger">
                                                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <strong>
                                                            Warning!</strong>
                                                        <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hdfID" runat="server" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="group" />
                                        <asp:HiddenField ID="hdfFlagID" runat="server" />
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
                        <div style="z-index: 1000; margin-left: -210px; margin-top: 100px; opacity: 1; -moz-opacity: 1;">
                            <img alt="" src="../../Resource/Images/Processing.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>
