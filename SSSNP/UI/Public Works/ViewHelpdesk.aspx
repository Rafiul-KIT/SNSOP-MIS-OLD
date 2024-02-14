<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="ViewHelpdesk" Title="::SNSDP Public Works   :: Helpdesk" Codebehind="ViewHelpdesk.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<%--<script src="resources/scripts/PopUpCalenderLeft.js" type="text/javascript"></script>--%>
    <script language="javascript">



function OpenPopup() 
 
   {

    window.open("Lookups/MobilizationLookup.aspx","List","scrollbars=yes,resizable=yes,width=900,height=750");

    return false;
    }
    
        
function Names()
    {
       var AsciiValue=event.keyCode
        if((AsciiValue>=65 && AsciiValue<=90) || (AsciiValue>=97 && AsciiValue<=122) || (AsciiValue==46) || (AsciiValue==32))
            event.returnValue=true;
        else
        {
            event.returnValue=false;
            
             alert("Enter Alphabets, '.' and Space Only");
           }
    }
    
    function DecimalOnly()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=48 && AsciiValue<=57 ) || (AsciiValue==8 || AsciiValue==127) || AsciiValue==46)
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                alert("Enter DecimalValues Only");
               }
        }
        

function AlphaNumericOnly()
{
       var AsciiValue=event.keyCode
        if((AsciiValue>=65 && AsciiValue<=90) || (AsciiValue>=97 && AsciiValue<=122) || (AsciiValue>=48 && AsciiValue<=57 ))
            event.returnValue=true;
        else
        {
            event.returnValue=false;
            
             alert("Enter Alphabets,  and Characters  Only");
           }
    }

    </script>

    <script type="text/javascript">
        $(function() {
        $('input[id$=txtration1]').bind('cut copy paste', function(e) {
                e.preventDefault();
                alert('You cannot ' + e.type + ' text!');
            });
        });
    </script>

    <script type="text/javascript">
        $(function() {
        $('input[id$=txtration2]').bind('cut copy paste', function(e) {
                e.preventDefault();
                alert('You cannot ' + e.type + ' text!');
            });
        });
    </script>

    <script type="text/javascript">
        $(function() {
        $('input[id$=txtsurveynum]').bind('cut copy paste', function(e) {
                e.preventDefault();
                alert('You cannot ' + e.type + ' text!');
            });
        });
    </script>

    <script type="text/javascript">
        $(function() {
        $('input[id$=txtExtent]').bind('cut copy paste', function(e) {
                e.preventDefault();
                alert('You cannot ' + e.type + ' text!');
            });
        });
    </script>

    <script type="text/javascript">
        $(function() {
        $('input[id$=txtCJFSBeneficiery]').bind('cut copy paste', function(e) {
                e.preventDefault();
                alert('You cannot ' + e.type + ' text!');
            });
        });


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
                                <i class="fa fa-fw fa-table"></i> Helpdesk
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> View Status
                            </li>
                        </ol>
     </div>
<div align="center">

 
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">View Status</h3>
                            </div>
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
                            <div class="panel-body">
                            
                                                    <table style="vertical-align: top; text-align: center;" cellpadding="0" cellspacing="0"
                                                        width="100%">
                                                        <tr>
                                                            <td align="center" 
                                                                style="padding: 5px; vertical-align: top; text-align: center" 
                                                                valign="middle">
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<table style="width: 96%;
                                                                    height: 53px" width="80%">
                                                                    <tr>
                                                                        <td style="width: 50%; height: 80px" valign="top">
                                                                            <table cellpadding="0" cellspacing="0" style="width: 50%">
                                                                                <tr>
                                                                                    <td align="right" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                        <asp:Label ID="lbldeptname2" runat="server" CssClass="LBLBLACK" Text="Feedback Type"
                                                                                            Width="110px"></asp:Label></td>
                                                                                    <td align="right" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 3%; padding-top: 5px; text-align: left"><asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Text=":"
                                                                                            Width="2px"></asp:Label></td>
                                                                                    <td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                        <asp:DropDownList ID="ddlfeedbak" runat="server" CssClass="DROPDOWN" 
                                                                                            AutoPostBack="True" 
                                                                                            TabIndex="1" ToolTip="Please select the FeedBack" ValidationGroup="group" 
                                                                                            Width="150px" onselectedindexchanged="ddlfeedbak_SelectedIndexChanged">
                                                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Text="NAME"
                                                                                            Width="110px"></asp:Label></td>
                                                                                    <td align="right" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 3%; padding-top: 5px; text-align: left">
                                                                                        <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Text=":"
                                                                                            Width="2px"></asp:Label></td>
                                                                                    <td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                        <asp:DropDownList ID="ddlPIA" runat="server" CssClass="DROPDOWN" 
                                                                                            AutoPostBack="True" 
                                                                                            TabIndex="1" ToolTip="Please Select PIA" ValidationGroup="group" 
                                                                                            Width="150px" OnSelectedIndexChanged="ddlPIA_SelectedIndexChanged">
                                                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width: 50%; height: 80px" valign="top">
                                                                            <table cellpadding="0" cellspacing="0" style="width: 50%; height: 40px">
                                                                                <tr>
                                                                                    <td align="right" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 2%; padding-top: 5px; text-align: left">
                                                                                        <asp:Label ID="lbldeptname3" runat="server" CssClass="LBLBLACK" Text="      Status"
                                                                                            Width="110px"></asp:Label></td>
                                                                                    <td align="right" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 2%; padding-top: 5px; text-align: left">
                                                                                        <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Text=":"
                                                                                            Width="2px"></asp:Label></td>
                                                                                    <td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 2%; padding-top: 5px; text-align: left">
                                                                                        <asp:DropDownList ID="ddlstat" runat="server" CssClass="DROPDOWN" 
                                                                                            AutoPostBack="True" 
                                                                                            TabIndex="1" ToolTip="Please select the FeedBack" ValidationGroup="group" 
                                                                                            Width="150px" onselectedindexchanged="ddlstat_SelectedIndexChanged">
                                                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                                                            <asp:ListItem Value="Open">Open</asp:ListItem>
                                                                                            <asp:ListItem>Closed</asp:ListItem>
                                                                                            <asp:ListItem>Rejected</asp:ListItem>
                                                                                        </asp:DropDownList></td>
                                                                                    <td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 2%; padding-top: 5px; text-align: left">
                                                                                        <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Text="Reference No"
                                                                                            Width="110px">
                                                                                        </asp:Label></td>
                                                                                    <td align="right" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 2%; padding-top: 5px; text-align: left">
                                                                                        <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Text=":"
                                                                                            Width="2px"></asp:Label></td>
                                                                                    <td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 2%; padding-top: 5px; text-align: left">
                                                                                        <asp:TextBox id="txtrefno" runat="server" CssClass="TXTBOX" MaxLength="15"
                                                                                             tabIndex="1" Width="150px" AutoPostBack="True" OnTextChanged="txtrefno_TextChanged"></asp:TextBox></td>
                                                                                    <td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <%-- <asp:TextBox ID="txtPrno0" runat="server" AutoPostBack="True" CssClass="TXTBOX" 
                                                                                            MaxLength="30" onkeypress="CharbarOnly()" OnTextChanged="txtPrno_TextChanged" 
                                                                                            TabIndex="1" Width="130px"></asp:TextBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td align="center" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                vertical-align: middle; padding-top: 5px; text-align: center" valign="middle">
                                                    <asp:Label ID="lblresult" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Green" Style="position: static"></asp:Label></td>
                                                        </tr>
                                                        <tr runat="server" id="Reject">
                                                            <td align="center" 
                                                                style="padding: 5px; vertical-align: middle; text-align: center" 
                                                                valign="middle">
                                                                <asp:Label ID="lblerrMsg" runat="server" Font-Bold="True" ForeColor="Red" 
                                                                    Width="270px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="Close">
                                                            <td align="center" 
                                                                style="padding: 5px; vertical-align: middle; text-align: center" 
                                                                valign="middle">
                                                                                        <asp:GridView ID="gvComplaintsList" runat="server" AutoGenerateColumns="False" 
                                                                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="4" Font-Names="Verdana" 
                                                                                            Font-Size="9pt" ForeColor="#333333" 
                                                                                            onselectedindexchanged="gvComplaintsList_SelectedIndexChanged" pagesize="30" 
                                                                                            Width="100%" AllowPaging="True" 
                                                                                            DataKeyNames="int_fbid,hd_Username,Fb_Type,hd_desc,hd_uplddocname,Status" 
                                                                                            onpageindexchanging="gvComplaintsList_PageIndexChanging" CssClass="TXTBOX" OnRowDataBound="gvComplaintsList_RowDataBound">
                                                                                            <RowStyle BackColor="#EFF3FB" />
                                                                                            <Columns>
<asp:HyperLinkField DataTextField="int_fbid" HeaderText="ID">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:HyperLinkField>
<asp:HyperLinkField DataTextField="hd_Code" HeaderText="Reference No">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:HyperLinkField>
<asp:BoundField DataField="hd_Username" HeaderText="User Name">
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Fb_Type" HeaderText="Feedback Type"></asp:BoundField>
<asp:BoundField DataField="hd_desc" HeaderText="Subject / Comments">
<HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Width="450px"></ItemStyle>
    <ControlStyle Width="450px" />
</asp:BoundField>
 <asp:TemplateField HeaderText="Uploaded Document">
                                                                                                    <EditItemTemplate>
                                                                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("hd_uplddocname") %>'></asp:TextBox>
                                                                                                    </EditItemTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:HyperLink ID="hypLetter" runat="server" Target="_blank"> </asp:HyperLink>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
<asp:BoundField DataField="Status" HeaderText="Status">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
                                                                                                <asp:BoundField DataField="regdate" HeaderText="Reg. Date" />
                                                                                                <asp:BoundField DataField="respdate" HeaderText="Date of Replay" />
<asp:TemplateField HeaderText="Change Status"><EditItemTemplate>
                                                                                                    
</EditItemTemplate>
<FooterTemplate>
                                                                                                        <asp:RequiredFieldValidator ID="rfvVehicle0" runat="server" 
                                                                                                            ControlToValidate="ddlVehicle" ErrorMessage="Please Select Vehicle" 
                                                                                                            InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                                                                                        <asp:DropDownList ID="ddlVehicle" runat="server" CssClass="DROPDOWN" 
                                                                                                            Width="82px">
                                                                                                        </asp:DropDownList>
                                                                                                    
</FooterTemplate>
<ItemTemplate>
<TABLE style="WIDTH: 35%; HEIGHT: 100%" cellSpacing=0 cellPadding=2 border=0><TBODY><TR><TD style="WIDTH: 100px; HEIGHT: 45px; TEXT-ALIGN: left" align=left><asp:DropDownList id="ddlchStatus" runat="server" Width="152px" CssClass="DROPDOWN" onselectedindexchanged="ddlchStatus_SelectedIndexChanged" ValidationGroup="group"><asp:ListItem>Open</asp:ListItem>
<asp:ListItem>Closed</asp:ListItem>
<asp:ListItem>Rejected</asp:ListItem>
</asp:DropDownList> </TD></TR><TR id="Pending" runat="server"><TD style="WIDTH: 100px; HEIGHT: 45px; TEXT-ALIGN: left" align=left><%--<asp:Button ID="Button1" runat="server" CssClass="BUTTON" Height="20px" 
                                                                                                                        onclick="Button1_Click" Text="Submit" Width="62px" />--%><asp:TextBox id="txtremarks" tabIndex=1 runat="server" Width="150px" CssClass="TXTBOX" ValidationGroup="group" Height="35px" MaxLength="250" TextMode="MultiLine"></asp:TextBox> </TD></TR><TR id="Close" runat="server"><TD style="PADDING-BOTTOM: 5px; PADDING-TOP: 5px; TEXT-ALIGN: center">&nbsp;<asp:Button id="BtnSave" tabIndex=10 onclick="BtnSave_Click" runat="server" Width="122px" Text="Change Status" CssClass="BUTTONLONG" ValidationGroup="group" ToolTip="To Save  the data" Height="20px"></asp:Button> </TD></TR></TBODY></TABLE>
</ItemTemplate>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:TemplateField>
                                                                                                <asp:BoundField DataField="anothermailid" Visible="False" />
</Columns>
                                                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                                            <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                                                            <EmptyDataTemplate>
<FONT color=red><B>No Records Found</B></FONT>
</EmptyDataTemplate>
                                                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                            <HeaderStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                                                            <EditRowStyle BackColor="#2461BF" />
                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                        </asp:GridView>
                                                                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" ForeColor="Red" 
                                                                    Width="272px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" 
                                                                style="padding: 5px; vertical-align: middle; height: 35px; text-align: left" 
                                                                valign="middle">
                                                                                        &nbsp; &nbsp;
                                                                                        </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" 
                                                                style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                vertical-align: middle; padding-top: 5px; height: 35px; text-align: center" 
                                                                valign="middle">
                                                                &nbsp;&nbsp;&nbsp;</td>
                                                        </tr>
                                                        </table>
                                                    <br />
                                                    <asp:ValidationSummary ID="vg" runat="server" ForeColor="Green" ShowMessageBox="True"
                                                        ShowSummary="False" Style="position: static" ValidationGroup="group" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hdfusercode" runat="server" />
                                        <asp:HiddenField ID="hdfID" runat="server" />
                                        <br />
                                        <asp:HiddenField ID="hdfFlagID" runat="server" />
                                        
                                         </ContentTemplate>
</asp:UpdatePanel>
                        </div>
                    </div>
                </div>

    </div>
    
   <%-- <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
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
</asp:Content>
