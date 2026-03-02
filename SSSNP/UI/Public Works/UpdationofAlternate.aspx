<%@ Page Title=":: :: SNSOP : : Beneficiary Enrollment " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="UpdationofAlternate" Codebehind="UpdationofAlternate.aspx.cs" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
     
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

       window.open("Lookups/BeneficiaryLookupAlternate.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

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


    function AlphaNumericOnly(evt) {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue >= 48 && AsciiValue <= 57))
            event.returnValue = true;
        else {
            event.returnValue = false;

            alert("Enter Alphabets,  and Characters  Only");
        }
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
                           
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Update Alternates</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Update Alternates</h3>
                            </div>
                            <div class="panel-body" >
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr >
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="185px">Search Beneficiary Details</asp:Label>
                                            <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px" 
                                                onclick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False" 
                                                Font-Size="12px" Style="position: static" ToolTip="Rate Lookup" />
                                                  &nbsp;
                                                <div  >
                                                 
                                                  </div> 
                                        </td>
                                         <td runat="server" id="trlookup" visible="false" style="padding: 1px; margin: 1px"  align="left">
                                          <a href="UploadsTrainee.aspx"  class="btn btn-primary">Upload Beneficiary Details</a>
                                         </td>
                                        
                                    </tr>
                                    <tr>
                                         
                                        <td style="padding: 5px; margin: 5px"  colspan="2" align="center">
                                            <asp:Label ID="lblEnrollmentNo" Font-Bold="true" Visible="false" runat="server" CssClass="LBLBLACK" Width="365px"></asp:Label>

                                        </td>
                                     
                                       
                                        
<td runat="server" id="Td1"  visible="false" style="padding: 1px; margin: 1px"  align="left">
                                               <asp:Label ID="lblhhno" Font-Bold="true" Visible="false" runat="server" CssClass="LBLBLACK" Width="465px"></asp:Label>
                                          <a href="UploadsTraineeUpdate.aspx" class="btn btn-primary">Upload Update Beneficiary Details</a>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top">
                                            &nbsp;</td>
                                    </tr>
                                       <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="244px">Beneficiary Details</asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top" align="left">
                                            <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="244px">First Alternate Payee Details</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                                            ControlToValidate="ddlIP" ErrorMessage="Please Select IP" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                

                                                
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label411" runat="server" CssClass="LBLBLACK" Width="160px">Household number</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtNameHN" runat="server" 
                                                            class="form-control txtbox" Height="28px" MaxLength="40" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>

                                     
                                              


                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="160px">Name of household head</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtHeadName" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                            ControlToValidate="txtHeadName" 
                                                            ErrorMessage="Please Enter Name of household head" SetFocusOnError="True" 
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                     
                                              


                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlSex" runat="server" 
                                                            class="form-control txtbox" Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                                            ControlToValidate="ddlSex" ErrorMessage="Please Select Sex" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label405" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlAge" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
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
                                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" 
                                                            class="form-control txtbox" Height="33px" 
                                                            onselectedindexchanged="ddlState_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                            ControlToValidate="ddlState" ErrorMessage="Please Select State" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label373" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" 
                                                            class="form-control txtbox" Height="33px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlCounties_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                            ControlToValidate="ddlCounties" ErrorMessage="Please Select County" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label404" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" 
                                                            class="form-control txtbox" Height="28px" Width="180px" 
                                                            AutoPostBack="True" onselectedindexchanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                            ControlToValidate="ddlPayam" ErrorMessage="Please Select Payam" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" OnSelectedIndexChanged="ddlBoma_SelectedIndexChanged" 
                                                            class="form-control txtbox" AutoPostBack="true" Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>

                                                         <asp:HiddenField ID="hdBoma" runat="server" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                            ControlToValidate="ddlBoma" ErrorMessage="Please Select Boma" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                </tr>

                                             
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image1" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                              
                                               
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                              <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label409" runat="server" CssClass="LBLBLACK" Width="137px">Household number</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtName1stAlternatePayeeHN" runat="server" 
                                                            class="form-control txtbox" Height="28px" MaxLength="40" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                              ControlToValidate="txtName1stAlternatePayeeHN" 
                                                              ErrorMessage="Please Enter First Alternate Payee Household number" InitialValue="" 
                                                              SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator></td>
                                                </tr>  
                                                  <tr>
                                                      <td style="padding: 5px; margin: 5px">
                                                          <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="137px">Name</asp:Label>
                                                      </td>
                                                      <td style="padding: 5px; margin: 5px">
                                                          :</td>
                                                      <td style="padding: 5px; margin: 5px; width: 192px;">
                                                          <asp:TextBox ID="txtName1stAlternatePayee" runat="server" 
                                                              class="form-control txtbox" Height="28px" MaxLength="40" TabIndex="1" 
                                                              ValidationGroup="group" Width="180px"></asp:TextBox>
                                                      </td>
                                                      <td style="padding: 5px; margin: 5px; width: 10px;">
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" 
                                                              ControlToValidate="txtName1stAlternatePayee" 
                                                              ErrorMessage="Please Enter First Alternate Payee" InitialValue="" 
                                                              SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label18" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA1Gender" runat="server" 
                                                            class="form-control txtbox" Width="180px" Height="28px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label19" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA1Age" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label28" runat="server" CssClass="LBLBLACK" Width="128px">National ID</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA1NationalID" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label22" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA1ContactNo" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label24" runat="server" CssClass="LBLBLACK" Width="165px">First Alternate Payee Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileUpload2" runat="server" class="form-control txtbox" 
                                                            Height="28px" onchange="this.form.submit();" />
                                                        <asp:Label ID="lblFileName1" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image2" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            &nbsp;</td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top" align="left">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label407" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="244px">Second Alternate Payee Details</asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="Label408" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="244px">Third Alternate Payee Details</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                               <table cellpadding="4" cellspacing="5" style="width: 83%">
                                             <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label25" runat="server" CssClass="LBLBLACK" Width="137px">Household number</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtName2ndAlternatePayeeHN" runat="server" 
                                                            class="form-control txtbox" Height="28px" MaxLength="40" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                              ControlToValidate="txtName2ndAlternatePayeeHN" 
                                                              ErrorMessage="Please Enter Second Alternate Payee Household number" InitialValue="" 
                                                              SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator></td>
                                                </tr>  
                                               <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="137px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtName2ndAlternatePayee" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="txtName2ndAlternatePayee" ErrorMessage="Please Enter Second Alternate Payee" 
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label20" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA2Gender" runat="server" 
                                                            class="form-control txtbox" Width="180px" Height="28px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label21" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA2Age" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label29" runat="server" CssClass="LBLBLACK" Width="128px">National ID</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA2NationalID" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label23" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA2ContactNo" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                
                                                  <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label26" runat="server" CssClass="LBLBLACK" Width="165px">Second Alternate Payee Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileUpload3" runat="server" class="form-control txtbox" 
                                                            Height="28px" onchange="this.form.submit();" />
                                                        <asp:Label ID="lblFileName2" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image3" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                 </table> </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top" align="left">
                                           <table cellpadding="4" cellspacing="5" style="width: 83%">
                                              <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label30" runat="server" CssClass="LBLBLACK" Width="137px">Household number</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtName1st3rdAlternatePayeeHN" runat="server" 
                                                            class="form-control txtbox" Height="28px" MaxLength="40" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                              ControlToValidate="txtName1st3rdAlternatePayeeHN" 
                                                              ErrorMessage="Please Enter Third Alternate Payee Household number" InitialValue="" 
                                                             Visible="false"  SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator></td>
                                                </tr>  
                                                  <tr>
                                                      <td style="padding: 5px; margin: 5px">
                                                          <asp:Label ID="Label31" runat="server" CssClass="LBLBLACK" Width="137px">Name</asp:Label>
                                                      </td>
                                                      <td style="padding: 5px; margin: 5px">
                                                          :</td>
                                                      <td style="padding: 5px; margin: 5px; width: 192px;">
                                                          <asp:TextBox ID="txtName13rdAlternatePayee" runat="server" 
                                                              class="form-control txtbox" Height="28px" MaxLength="40" TabIndex="1" 
                                                              ValidationGroup="group" Width="180px"></asp:TextBox>
                                                      </td>
                                                      <td style="padding: 5px; margin: 5px; width: 10px;">
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                              ControlToValidate="txtName13rdAlternatePayee" 
                                                              ErrorMessage="Please Enter Third Alternate Payee" InitialValue="" 
                                                           Visible="false"    SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label32" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA3Gender" runat="server" 
                                                            class="form-control txtbox" Width="180px" Height="28px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label33" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA3Age" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label34" runat="server" CssClass="LBLBLACK" Width="128px">National ID</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA3NationalID" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label35" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA3ContactNo" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label36" runat="server" CssClass="LBLBLACK" Width="165px">Third Alternate Payee Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileUpload4" runat="server" class="form-control txtbox" 
                                                            Height="28px" onchange="this.form.submit();" />
                                                        <asp:Label ID="lblFileName4" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image4" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                </table>  </td>
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
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label410" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="244px">Payment Details</asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" CssClass="GRD table-bordered " ForeColor="#333333" 
                                                GridLines="None" Height="62px" Width="100%">
                                                <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                                <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                                    verticalalign="Middle" />
                                                <columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <%--<ItemStyle Width="50px" />--%></asp:TemplateField>
                                                    <asp:BoundField DataField="WorkCode" HeaderText="Work Code" 
                                                        ItemStyle-Width="170px">
                                                        <ItemStyle Width="170px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RequestNo" HeaderText="Request No" />
                                                    <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                                    <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                                    
                                                    <asp:BoundField DataField="PaidDates" HeaderText="Paid Date" />
                                                         <asp:BoundField DataField="PaidToType" HeaderText="Paid To Type" /> 
                                                    <asp:BoundField DataField="Paidto" HeaderText="Paid To Household number" />
                                              
                                                    <asp:BoundField DataField="PaidToName" HeaderText="Paid To Name" 
                                                        ItemStyle-Width="150px">
                                                        <ItemStyle Width="150px" />
                                                    </asp:BoundField>
                                                </columns>
                                                <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                                <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                                <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                                    forecolor="White" Height="60px" />
                                                <editrowstyle backcolor="#B9D684" />
                                                <alternatingrowstyle backcolor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Save" 
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnDelete" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-warning" Height="32px" onclick="BtnClear0_Click" 
                                                OnClientClick="return confirm('Do you want to delete the record ? ');" 
                                                TabIndex="10" Text="Delete" Visible="false" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-danger" Height="32px" onclick="BtnClear_Click" TabIndex="10" 
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
                                            
                                            <div ID="Failure" runat="server" class="alert alert-danger" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>
                                                Warning!</strong>
                                                <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                            </div>
                                            <div style="color:Red">
                                            <asp:Label ID="lblresult" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile1" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile2" runat="server" />
                                                                <asp:HiddenField ID="hdfUploadFile4" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile3" runat="server" />
                                 <asp:HiddenField ID="hdfa1" runat="server" />
                                  <asp:HiddenField ID="hdfa2" runat="server" />
                                   <asp:HiddenField ID="hdfa3" runat="server" />
                                         <asp:HiddenField ID="hdfahno1" runat="server" />
                                  <asp:HiddenField ID="hdfahno2" runat="server" />
                                   <asp:HiddenField ID="hdfahno3" runat="server" />
                                         <asp:HiddenField ID="hdfahname1" runat="server" />
                                  <asp:HiddenField ID="hdfahname2" runat="server" />
                                   <asp:HiddenField ID="hdfahname3" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                            </div>
                        </div>
                    </div>
                </div>

    </div>
               
                 <%-- <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
<ProgressTemplate>
<div class="overlay">

<div style=" z-index: 1000; margin-left: -210px;margin-top:100px;opacity: 1; -moz-opacity: 1;">
<img alt="" src="../../Resource/Images/Processing.gif" />

</div>
    
</div>
</ProgressTemplate>
</asp:UpdateProgress>--%>

         
                       
  </ContentTemplate>
  </asp:UpdatePanel>
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

