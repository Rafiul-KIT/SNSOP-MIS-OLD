<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/UI/Public Works/CCMaster.master" Inherits="Payingagent" Codebehind="Payingagent.aspx.cs" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
    
<style type="text/css">
.overlay
{
position: fixed;
z-index: 999;
height: 100%;
width: 100%;
top: 1px;
background-color:Gray;
filter: alpha(opacity=60);
opacity: 0.9;
-moz-opacity: 0.9;
}
</style>
    <script type="text/javascript" language="javascript">

function OpenPopup() 
 
   {

    window.open("Lookups/PAlookup.aspx","List","scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

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
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="Home.aspx">Dashboard</a>
                            </li>
                            <li class="">
                                <i class="fa fa-fw fa-table"></i> Masters
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Paying Agent</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Paying Agent</h3>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
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
                                             <%--<tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label357" runat="server" CssClass="LBLBLACK" Width="165px">Ministry</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlministry" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>MoAFCRD</asp:ListItem>
                                                            <asp:ListItem>MoLPSHRD</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="ddlministry" ErrorMessage="Please Select Ministry" 
                                                            ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator>&nbsp;</td>
                                                </tr>--%>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Name of Paying Agent</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtname" runat="server" class="form-control txtbox" 
                                                            Height="40px" MaxLength="40" onkeypress="Names()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="txtname" ErrorMessage="Please Enter Name Of Paying Agent" 
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="137px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlstate" runat="server" class="form-control txtbox" 
                                                            Height="33px" AutoPostBack="True" onselectedindexchanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Central Equatoria</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="ddlState" ErrorMessage="Please Select State" 
                                                            ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label358" runat="server" CssClass="LBLBLACK" Width="165px">County 
                                                        Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox" 
                                                            Height="33px" AutoPostBack="True" onselectedindexchanged="ddlCounties_SelectedIndexChanged" >
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="ddlCounties" ErrorMessage="Please Select County" 
                                                            ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator> &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label350" runat="server" CssClass="LBLBLACK" Width="137px">Payams Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlpayams" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px" AutoPostBack="True">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                            
                                                            
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                            ControlToValidate="ddlpayams" ErrorMessage="Please Select Payams" 
                                                            ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator> &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label372" runat="server" CssClass="LBLBLACK" Width="137px">Address</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtaddress" runat="server" class="form-control txtbox" 
                                                            Height="40px" MaxLength="50"  TabIndex="1" 
                                                            TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="137px">Email</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtemail" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                            ControlToValidate="txtemail" ErrorMessage="Please Enter Email" 
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator
                                                                ID="RegularExpressionValidator1" runat="server" 
                                                            ErrorMessage="Please Enter Correct Email" ValidationGroup="group" 
                                                            ControlToValidate="txtemail" 
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator> &nbsp;</td>
                                                </tr>
                                                <%--<tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="137px">User Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlUserType" runat="server" 
                                                            class="form-control txtbox" Height="28px" Width="180px" >
                                                            <asp:ListItem Value="--Select--" >--Select--</asp:ListItem>
                                                            <asp:ListItem Value="TST" >TST</asp:ListItem>
                                                            <asp:ListItem Value="Cordinator">PIU-Coordinator</asp:ListItem>
                                                            <asp:ListItem Value="Specialist">PIU-Specialist</asp:ListItem>
                                                            <asp:ListItem Value="Manager">PIU-Manager</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                            ControlToValidate="ddlUserType" ErrorMessage="Please Select User Type" 
                                                            ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator> &nbsp;</td>
                                                </tr>--%>

                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label361" runat="server" CssClass="LBLBLACK" Width="137px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtcontact" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                            ControlToValidate="txtcontact" ErrorMessage="Please Enter Contcat" 
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator> 
                                                        &nbsp;</td>
                                                </tr>
                                               <%-- <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label375" runat="server" CssClass="LBLBLACK" Width="137px">Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddltype" runat="server" 
                                                            class="form-control txtbox" Height="28px" Width="180px" 
                                                            AutoPostBack="True" onselectedindexchanged="ddltype_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="N" >National Level Team</asp:ListItem>
                                                            <asp:ListItem Value="S">State Level Team</asp:ListItem>
                                                            <asp:ListItem Value="C">County Level Team</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                            ControlToValidate="ddltype" ErrorMessage="Please Select Type" 
                                                            ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator> &nbsp;</td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label360" runat="server" CssClass="LBLBLACK" Width="137px">Designation</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddldesignation" runat="server" 
                                                            class="form-control txtbox" Height="28px" Width="180px" 
                                                            AutoPostBack="True">
                                                            <asp:ListItem>--Select--</asp:ListItem>                                                          
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                            ControlToValidate="ddldesignation" ErrorMessage="Please Select Designations" 
                                                            ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator> &nbsp;</td>
                                                </tr>--%>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label362" runat="server" CssClass="LBLBLACK" Width="137px">Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlstatus" runat="server" 
                                                            class="form-control txtbox" Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="A">Active</asp:ListItem>
                                                            <asp:ListItem Value="I">InActive</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                            ControlToValidate="ddlstatus" ErrorMessage="Please Select Status" 
                                                            ValidationGroup="group" InitialValue="--Select--">*</asp:RequiredFieldValidator>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label373" runat="server" CssClass="LBLBLACK" Width="137px">Upload 
                                                        Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileUpload1"  runat="server" onchange="this.form.submit();" class="form-control txtbox" 
                                                            Height="28px" />
                                                        <asp:Label ID="lblFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image1" runat="server" Height="128px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="191px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            <asp:Label ID="Label365" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="165px">Login Details</asp:Label>
                                        </td>
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
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">User 
                                                        Id</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtuser1" AutoCompleteType="disabled" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="10"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                                            ControlToValidate="txtuser1" ErrorMessage="Please Enter User ID" 
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="137px">Password</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtpass1" AutoCompleteType="disabled" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="10"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px" TextMode="Password"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                            ControlToValidate="txtpass1" ErrorMessage="Please Enter Password" 
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>&nbsp;</td>
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
                                                Height="32px" TabIndex="10" Text="Save" 
                                                ValidationGroup="group" Width="90px" onclick="BtnSave1_Click" />
                                            &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-danger" Height="32px"  TabIndex="10" 
                                                Text="ClearAll" ToolTip="To Clear  the Screen" Width="90px" 
                                                onclick="BtnClear_Click" />
                                                &nbsp;<asp:Button ID="BtnDelete" Visible="false" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-danger" Height="32px" TabIndex="10" 
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
                                
                                <asp:HiddenField ID="hdfUploadFile1" runat="server" />
                                
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                            </div>
                              </ContentTemplate>
</asp:UpdatePanel>
                             <asp:HiddenField ID="HiddenFieldFilePath" runat="server" />
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
