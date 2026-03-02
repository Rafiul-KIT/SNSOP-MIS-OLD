<%@ Page Title=":: :: SNSOP : : Helpdesk Registration" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="Helpdesk" Codebehind="Helpdesk.aspx.cs" %>


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
        .style5
        {
            width: 159px;
        }
        .style6
        {
            width: 4px;
        }
        .style7
        {
            width: 734px;
        }
    </style>
    <script type="text/javascript" language="javascript">

function OpenPopup() 
 
   {

       window.open("Lookups/Bomas.aspx", "List", "scrollbars=yes,resizable=yes,width=920,height=650;display = block;position=absolute");

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
                                <i class="fa fa-dashboard"></i>  <a href="index.html">Dashboard</a>
                            </li>
                            <li class="">
                                <i class="fa fa-fw fa-table"></i> Masters
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> Boma / Quarter Council
                            </li>
                        </ol>
     </div>
<div align="center">

 
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Helpdesk Registration</h3>
                            </div>
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
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
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lbldeptname" runat="server" CssClass="LBLBLACK" Width="165px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label57" runat="server" CssClass="LBLBLACK" Width="165px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="165px">User Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label58" runat="server" CssClass="LBLBLACK" Width="165px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label352" runat="server" CssClass="LBLBLACK" Width="165px">Officer Mail Id</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtemail" runat="server" class="form-control txtbox"   MaxLength="40" TextMode="MultiLine" 
                                                                    Width="180px" TabIndex="1"></asp:TextBox>
                                                        <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail"
                                                                    ErrorMessage="Please Correct Email of Trainee" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ValidationGroup="group">*</asp:RegularExpressionValidator>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                                            ControlToValidate="txtemail" ErrorMessage="Enter Officer Mail id" 
                                                                                            ValidationGroup="group"><span style="color: red;">&ensp;*</span></asp:RequiredFieldValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="165px">Feedback Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlfeedback" runat="server" class="form-control txtbox" Height="30px"
                                                                                            AutoPostBack="True" 
                                                                                            TabIndex="1" ToolTip="Please select the FeedBack" ValidationGroup="group" 
                                                                                            Width="180px">
                                                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                            
                                                            
                                                   
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                                            ControlToValidate="ddlfeedback" ErrorMessage="Select the Feedback" 
                                                                                            InitialValue="--Select--" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label355" runat="server" CssClass="LBLBLACK" Width="113px">Upload</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                       <asp:FileUpload ID="txtupload" runat="server" class="form-control txtbox"  Height="33px" 
                                                                                            onchange="this.form.submit();" tabIndex="1" Width="180px" /><br />
                                                                                        <asp:Label id="lblScanLetter" runat="server" CssClass="LBLBLACKFOOTER"></asp:Label> &nbsp;<asp:HyperLink id="hypScanLetter" runat="server" CssClass="LBLBLACKFOOTER" Target="_blank">[hypScanLetter]</asp:HyperLink>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td class="style5" style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lbldeptname0" runat="server" CssClass="LBLBLACK" Width="165px">Name</asp:Label>
                                                    </td>
                                                    <td class="style6" style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td class="style7" style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtsubjet" runat="server" class="form-control txtbox" 
                                                            Height="69px" MaxLength="150" TabIndex="1" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                                            ControlToValidate="txtsubjet" ErrorMessage="Enter the Subject/Comments" 
                                                                                            ValidationGroup="group"><span style="color: red;">&ensp;*</span></asp:RequiredFieldValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <asp:Button ID="BtnSave" runat="server" Height="32px" CssClass="btn btn-primary"   TabIndex="10"
                                                                    Text="Submit" ToolTip="To Save  the data" ValidationGroup="group" OnClick="BtnSave_Click" />&nbsp;&nbsp;<asp:Button
                                                                        ID="BtnClosebatchClear" Height="32px" runat="server" CausesValidation="False" CssClass="btn btn-warning"
                                                                         TabIndex="10" Text="ClearAll" ToolTip="To Clear  the Screen" Width="100px"
                                                                        OnClick="BtnClear_Click" />
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
          
                       
  </ContentTemplate>
  </asp:UpdatePanel>
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

