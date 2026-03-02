<%@ Page Title=":: :: SNSOP : : Employer Details " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="HeadEmployerRegistration" Codebehind="HeadEmployerRegistration.aspx.cs" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../Resource/js/jquery.js" type="text/javascript"></script>
    <script src="../Resource/js/bootstrap.min.js"></script>   
    <script src="../Resource/js/plugins/morris/raphael.min.js"></script>
    <script src="../Resource/js/plugins/morris/morris.min.js"></script>
    <script src="../Resource/js/plugins/morris/morris-data.js"></script>


    <script type="text/javascript" language="javascript">

function OpenPopup() 
 
   {

    window.open("Lookups/CurriculamLookUp.aspx","List","scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

    return false;
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
                                <i class="fa fa-fw fa-edit"></i> Head Office
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Employer Details</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Employer Details</h3>
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
                                                        <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Employer Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtActEndDate1" runat="server" class="form-control txtbox" 
                                                            Height="40px" MaxLength="40" onkeypress="Names()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="137px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlgender2" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label358" runat="server" CssClass="LBLBLACK" Width="165px">Counties Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlgender1" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label350" runat="server" CssClass="LBLBLACK" Width="137px">Payams Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlgender0" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                            
                                                            
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="137px">Bomas Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlgender" runat="server" 
                                                            class="form-control txtbox" Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:DropDownList>
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
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label371" runat="server" CssClass="LBLBLACK" Width="137px">Address</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtActEndDate2" runat="server" class="form-control txtbox" 
                                                            Height="40px" MaxLength="40" onkeypress="Names()" TabIndex="1" 
                                                            TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label360" runat="server" CssClass="LBLBLACK" Width="137px">Contact Person</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtActEndDate0" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" onkeypress="Names()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="137px">Email</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlSkill" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Domain</asp:ListItem>
                                                            <asp:ListItem>Non Domain</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label361" runat="server" CssClass="LBLBLACK" Width="137px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="DropDownList14" runat="server" 
                                                            class="form-control txtbox" Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label362" runat="server" CssClass="LBLBLACK" Width="137px">Course Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="DropDownList13" runat="server" 
                                                            class="form-control txtbox" Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
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
                                                CssClass="btn btn-warning" Height="32px" onclick="BtnClear_Click" TabIndex="10" 
                                                Text="ClearAll" ToolTip="To Clear  the Screen" Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

    </div>
               
                 
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

