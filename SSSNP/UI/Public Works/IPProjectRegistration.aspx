<%@ Page Title=":: :: SSSNP : : Project Details " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="IPProjectRegistration" Codebehind="IPProjectRegistration.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
    <script src="../Resource/js/jquery.js" type="text/javascript"></script>
    <script src="../Resource/js/bootstrap.min.js"></script>   
    <script src="../Resource/js/plugins/morris/raphael.min.js"></script>
    <script src="../Resource/js/plugins/morris/morris.min.js"></script>
    <script src="../Resource/js/plugins/morris/morris-data.js"></script>
    
    <script src="../../Resource/js/bootstrap-datetimepicker.js" type="text/javascript"></script>
     
<script>
    $(document).ready(function() {

        var rigtDynHt = $('.form-left-pan').height();
        //alert(rigtDynHt);
        $('.form-right-pan').css("height", rigtDynHt);

        $('#txtStartDate').datetimepicker();
    });
	</script>
    <script type="text/javascript" language="javascript">

function OpenPopup() 
 
   {

       window.open("Lookups/ProjectLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

    return false;
        }

        function ApplyChanges(val, val1) {

            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

            document.forms[0].submit();
        }
    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>
<div align="left">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="Home.aspx">Dashboard</a>
                            </li>
                            <li class="">
                                <i class="fa fa-fw fa-edit"></i> Implementing Partner
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Project Registration</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Project Registration</h3>
                            </div>
                            <div class="panel-body" >
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="165px">Search</asp:Label>
                                            <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px" 
                                                onclick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False" 
                                                Font-Size="12px" Style="position: static" ToolTip="Rate Lookup" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            &nbsp;</td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px" ValidationGroup="group">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                                                            ControlToValidate="ddlIP" ErrorMessage="Please Select IP" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="137px">Project Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtProjectName" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                                            ControlToValidate="txtProjectName" ErrorMessage="Please Enter Project Name" 
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label350" runat="server" CssClass="LBLBLACK" Width="137px">Start Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="9" onkeypress="Dates()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
Format="dd-MM-yyyy" PopupButtonID="txtStartDate" 
TargetControlID="txtStartDate">
</cc1:CalendarExtender>


<%--<input type="date" value="Select date" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Select date';}">--%>
                                                            
                                                           </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" 
                                                            ControlToValidate="txtStartDate" ErrorMessage="Please Enter Start Date" 
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="137px">End Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtEndDate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="10" onkeypress="Dates()"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
Format="dd-MM-yyyy" PopupButtonID="txtEndDate" 
TargetControlID="txtEndDate">
</cc1:CalendarExtender>
                                                            
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" 
                                                            ControlToValidate="txtEndDate" ErrorMessage="Please Enter End Date" 
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label359" runat="server" CssClass="LBLBLACK" Width="137px">Project Sanction Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtActStartDate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="10" onkeypress="Dates()"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtActStartDate_CalendarExtender" runat="server" 
                                                            Format="dd-MM-yyyy" PopupButtonID="txtActStartDate" 
                                                            TargetControlID="txtActStartDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" 
                                                            ControlToValidate="txtActStartDate" ErrorMessage="Please Enter Sanction Date" 
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px" colspan="3" align="left">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="3" style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label369" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="267px">State / County wise Project Details</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlState" runat="server" 
                                                            class="form-control txtbox" Height="33px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                                                            ControlToValidate="ddlState" ErrorMessage="Please Select State" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="child">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                
                                                <tr runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label374" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlPayams" runat="server" 
                                                            class="form-control txtbox" Height="33px" 
                                                            onselectedindexchanged="ddlPayams_SelectedIndexChanged" 
                                                            AutoPostBack="True">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                                                            ControlToValidate="ddlWorkActivity" ErrorMessage="Please Select Work Activity" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="child" 
                                                            Visible="False">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" 
                                                            class="form-control txtbox" Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                                                            ControlToValidate="ddlWorkActivity" ErrorMessage="Please Select Work Activity" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="child" 
                                                            Visible="False">*</asp:RequiredFieldValidator>
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
                                                        <asp:Label ID="Label361" runat="server" CssClass="LBLBLACK" Width="137px">Target Beneficiaries</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtTargetBen" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" 
                                                            ControlToValidate="txtTargetBen" 
                                                            ErrorMessage="Please Enter Target Beneficiaries" SetFocusOnError="True" 
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr >
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label362" runat="server" CssClass="LBLBLACK" Width="137px">Project Cost (In USD)</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtPrjCost" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="17" onkeypress="DecimalOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" 
                                                            ControlToValidate="txtPrjCost" ErrorMessage="Please Enter Project Cost (In USD)" 
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label352" runat="server" CssClass="LBLBLACK" Width="152px">Person Incharge of the Project</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtPrjInchargeName" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" onkeypress="Names()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" 
                                                            ControlToValidate="txtPrjInchargeName" 
                                                            ErrorMessage="Please Enter Incharge Name" SetFocusOnError="True" 
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label298" runat="server" CssClass="LBLBLACK" Width="152px">Person Incharge Mobile </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtPrjInchMobile" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" 
                                                            ControlToValidate="txtPrjInchMobile" 
                                                            ErrorMessage="Please Enter Person Incharge Mobile" SetFocusOnError="True" 
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="152px">Person Incharge Email</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtPrjInchEmail" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" 
                                                            ControlToValidate="txtPrjInchEmail" ErrorMessage="Please Enter Project Email" 
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                            ControlToValidate="txtPrjInchEmail" ErrorMessage="Please Enter Valid Email" 
                                                            SetFocusOnError="True" 
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                            ValidationGroup="group">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label363" runat="server" CssClass="LBLBLACK" Width="124px">Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>InProgress</asp:ListItem>
                                                            <asp:ListItem>Closed</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
                                                            ControlToValidate="ddlStatus" ErrorMessage="Please Select Status" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label373" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" 
                                                            class="form-control txtbox" Height="33px" 
                                                            onselectedindexchanged="ddlCounties_SelectedIndexChanged" 
                                                            AutoPostBack="True">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                                            ControlToValidate="ddlCounties" ErrorMessage="Please Select Municipality" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="child">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label367" runat="server" CssClass="LBLBLACK" Width="165px">Proposed Target</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtProposedTarget" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="5" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="child" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                                            ControlToValidate="txtProposedTarget" 
                                                            ErrorMessage="Please Enter Proposed Target" SetFocusOnError="True" 
                                                            ValidationGroup="child" Visible="False">*</asp:RequiredFieldValidator>
                                                        </td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="137px">Type of Work</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlAreasofWork"  runat="server" 
                                                            class="form-control txtbox" Height="28px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlAreasofWork_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                                                            ControlToValidate="ddlAreasofWork" ErrorMessage="Please Select Type of Work" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="child" 
                                                            Visible="False">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label372" runat="server" CssClass="LBLBLACK" Width="137px">Work 
                                                        Activity</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlWorkActivity" runat="server" 
                                                            class="form-control txtbox" Height="28px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                                            ControlToValidate="ddlWorkActivity" ErrorMessage="Please Select Work Activity" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="child" 
                                                            Visible="False">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px" colspan="2">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-xs btn-warning" 
                                                            Height="28px" TabIndex="10" Text="Add New" ValidationGroup="child" 
                                                            Width="72px" onclick="BtnSave2_Click" />
                                                        &nbsp;<asp:Button ID="BtnClear0" runat="server" CausesValidation="False" 
                                                            CssClass="btn btn-xs btn-danger" Height="28px" TabIndex="10" Text="Cancel" 
                                                            ToolTip="To Clear  the Screen" Width="73px" onclick="BtnClear0_Click" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <asp:GridView ID="gvpractical0" runat="server" AutoGenerateColumns="False" 
                                                BorderColor="#003399" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                CssClass="GRD" DataKeyNames="intPrjid,intPrjtarget" Font-Names="Verdana" 
                                                Font-Size="12px" ForeColor="#333333" GridLines="None" 
                                                OnRowDeleting="gvpractical0_RowDeleting" Width="100%">
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#FFFFFF" />
                                                <Columns>
                                                    <asp:CommandField HeaderText="Edit" ShowSelectButton="True" Visible="False" />
                                                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                                                    <asp:BoundField DataField="StateName" HeaderText="State" />
                                                    <asp:BoundField DataField="CountieName" HeaderText="County / Municipality" />
                                                    <asp:BoundField DataField="PayamName" HeaderText="Payam / Block" Visible="false" />
                                                    <asp:BoundField DataField="BomaName" HeaderText="Boma / Quarter Council " Visible="false" />
                                                    <asp:BoundField DataField="Proposedtarget" HeaderText="Proposed Target" 
                                                       Visible="false" />
                                                    <asp:BoundField DataField="AreaofWork" HeaderText="Type of Work" Visible="false" />
                                                    <asp:BoundField DataField="WorkActivity" HeaderText="WorkActivity" Visible="false" />
                                                </Columns>
                                                <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#013161" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Save" 
                                                ValidationGroup="group" Width="90px" />
                                            <asp:Button ID="BtnClear" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-warning" Height="32px" onclick="BtnClear_Click" TabIndex="10" 
                                                Text="ClearAll" ToolTip="To Clear  the Screen" Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <div ID="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" 
                                                    href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label 
                                                    ID="lblmsg" runat="server"></asp:Label>
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
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="child" />
                            </div>
                        </div>
                    </div>
                </div>

    </div>
               <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
<ProgressTemplate>
<div class="overlay">
<%--<div style=" z-index: 1000; margin-left: 350px;margin-top:200px;opacity: 1;-moz-opacity: 1;">--%>
<div style=" z-index: 1000; margin-left: -210px;margin-top:100px;opacity: 1; -moz-opacity: 1;">
<img alt="" src="../../Resource/Images/Processing.gif" />

</div>
    
</div>
</ProgressTemplate>
</asp:UpdateProgress>
                 
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          
                       
  </ContentTemplate>
  </asp:UpdatePanel>
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

