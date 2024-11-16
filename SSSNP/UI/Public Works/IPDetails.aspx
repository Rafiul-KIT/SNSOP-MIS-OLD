<%@ Page Title=":: :: SNSOP : : Implentation Partner Details" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="IPDetails" Codebehind="IPDetails.aspx.cs" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
    <style type="text/css">
.overlay
{
position: fixed;
z-index: 999;
height: 100%;
width: 100%;
top: 112px;
background-color:Gray;
filter: alpha(opacity=60);
opacity: 0.9;
-moz-opacity: 0.9;
}
</style>
    <script type="text/javascript" language="javascript">



function OpenPopup() 
 
   {

    window.open("Lookups/LookupTSTI.aspx","List","scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

    return false;
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
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="Home.aspx">Dashboard</a>
                            </li>
                            <li class="">
                                <i class="fa fa-fw fa-edit"></i> IP
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Implementation Partner Details</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Implementation Partner Details</h3>
                            </div>
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                            <div class="panel-body" >
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            &nbsp;</td>
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
                                                        <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Name 
                                                        of the IP</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtip" runat="server" class="form-control txtbox" 
                                                            Height="42px" MaxLength="40" onkeypress="Names()" TabIndex="1" 
                                                            TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="txtip" ErrorMessage="Please Enter IP Name" 
                                                            ValidationGroup="group" >*</asp:RequiredFieldValidator> &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="137px">Type of IP</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddltypeip" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="P">Private</asp:ListItem>
                                                            <asp:ListItem Value="N">NGO</asp:ListItem>
                                                              <asp:ListItem Value="U">UN Agencies</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="ddltypeip" ErrorMessage="Please Select Type of IP" 
                                                            ValidationGroup="group" InitialValue="--Select--" >*</asp:RequiredFieldValidator> &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label349" runat="server" CssClass="LBLBLACK" Width="137px">Authorised Person</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtauthorised" runat="server" class="form-control txtbox" 
                                                            Height="35px" MaxLength="40" onkeypress="Names()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="txtauthorised" ErrorMessage="Please Enter Authorised Person" 
                                                            ValidationGroup="group" >*</asp:RequiredFieldValidator>   &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="137px">Authorised 
                                                        Person Email Id</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtmail" runat="server" class="form-control txtbox" 
                                                            Height="34px" MaxLength="40"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="txtmail" 
                                                            ValidationGroup="group" ErrorMessage="Please Enter Email" >*</asp:RequiredFieldValidator> 
                                                            <asp:RegularExpressionValidator
                                                                ID="RegularExpressionValidator1" runat="server" 
                                                            ErrorMessage="Please Enter Correct Email" ValidationGroup="group" 
                                                            ControlToValidate="txtmail" 
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="137px">Authorised 
                                                        Person Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtcontact1" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                            ControlToValidate="txtcontact1" ErrorMessage="Please Enter Contact" 
                                                            ValidationGroup="group" Visible="False">*</asp:RequiredFieldValidator>
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
                                                        <asp:Label ID="Label369" runat="server" CssClass="LBLBLACK" Width="137px">Secondary Authorised Person</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtauthorised0" runat="server" class="form-control txtbox" 
                                                            Height="35px" MaxLength="40" onkeypress="Names()" TabIndex="1" 
                                                            TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label371" runat="server" CssClass="LBLBLACK" Width="137px">Secondary Authorised 
                                                        Person Email Id</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtSEmail" runat="server" class="form-control txtbox" 
                                                            Height="34px" MaxLength="40"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                            ControlToValidate="txtSEmail" ErrorMessage="Please Enter Correct Email" 
                                                            SetFocusOnError="True" 
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                                            ValidationGroup="group">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label366" runat="server" CssClass="LBLBLACK" Width="145px">Secondary Authorised 
                                                        Person Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtScontact" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label355" runat="server" CssClass="LBLBLACK" Width="124px">Address</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtaddress" runat="server" class="form-control txtbox" 
                                                            Height="40px" MaxLength="150"  TabIndex="1" 
                                                            TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label356" runat="server" CssClass="LBLBLACK" Width="124px">Website</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtweb" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" onkeypress="Names()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                            ControlToValidate="txtweb" ErrorMessage="Please Enter Website" 
                                                            ValidationGroup="group" >*</asp:RequiredFieldValidator>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                                            runat="server" ControlToValidate="txtweb" 
                                                            ErrorMessage="Please Enter Valid website" 
                                                            ValidationExpression="([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?" 
                                                            ValidationGroup="group">*</asp:RegularExpressionValidator>
                                                             &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Save" 
                                                ValidationGroup="group" Width="90px" />
                                            <asp:Button ID="BtnClear" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-danger" Height="32px" onclick="BtnClear_Click" TabIndex="10" 
                                                Text="ClearAll" ToolTip="To Clear  the Screen" Width="90px" />
                                                <asp:Button ID="BtnDelete" Visible="false" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-danger" Height="32px" onclick="BtnClear0_Click" TabIndex="10" 
                                                Text="Delete" Width="90px" 
                                                OnClientClick="return confirm('Do you want to delete the record ? ');" />
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

