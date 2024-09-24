<%@ Page Language="C#" MasterPageFile="CCMaster.master" AutoEventWireup="true" Inherits="Home" Title=":: :: SSSNP : :: Home" Codebehind="Home.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <script language="javascript">



function OpenPopup() 
 
   {

    window.open("Lookups/frmcjfslookup.aspx","List","scrollbars=yes,resizable=yes,width=550,height=320");

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
    
    
   
<%--<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;" valign="top" align="center">
       </td>
        </tr>
    </table>--%>
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
    <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                             <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">

                                            <div class="panel panel-default">
                                                <div class="panel-heading" align="center">
                                                  <asp:HiddenField ID="hasflash" runat="server" />
                                                    <table cellpadding="4" cellspacing="5">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label350" runat="server" CssClass="LBLBLACK">Year</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:DropDownList ID="ddlYear" runat="server" class="form-control txtbox"
                                                                    Height="33px" Width="100px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                                                    <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                                                    <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                        </tr>

                                                    </table>
                                                </div>
                                                <div class="panel-body" align="center">

                                                    <table cellpadding="3" cellspacing="5" style="width: 100%">
                                                        <tr>
                                                            <td style="width: 445px">
                                                                <div class="list-group">
                                                                       <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lblNameOfIP" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-calendar"></i>Name of Implementing Partners
                                                                    </a>
                                                                    <a href='#'  class="list-group-item">
                                                                        <span class="badge" style="background-color: #d9534f;">
                                                                            <asp:Label ID="ImplementingStateCount"
                                                                                runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-truck"></i>No. of Implementing States
                                                                    </a>
                                                                     <a href='#'  class="list-group-item">
                                                                        <span class="badge" style="background-color: #d9534f;">
                                                                            <asp:Label ID="ImplementingCountyCount"
                                                                                runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-truck"></i>No. of Implementing Counties
                                                                    </a>
                                                                     <a href='#' target="_blank"  class="list-group-item">
                                                                        <span class="badge" style="background-color: #d9534f;">
                                                                            <asp:Label ID="ImplementingPayamCount"
                                                                                runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-money"></i>No. of Implementing Payams
                                                                    </a>
                                                                      <a href='#'  target="_blank"  class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="ImplementingBomaCount" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Number of Implementing Bomas
                                                                    </a>
                                                                </div>
                                                            </td>
                                                            <td style="width: 80px">&nbsp;</td>
                                                            <td valign="top">
                                                                <div class="list-group">                                                                    
                                                                    <a href='#'  target="_blank"  class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="LabelTotalBeneficiaryCount" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Total Beneficiaries
                                                                    </a>
                                                                    <a href='#'  target="_blank"  class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="LabelPendingBeneficiaryCount" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Pending Beneficiaries
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="LblEnBen" runat="server"></asp:Label>
                                                                        </span>
                                                                        <i class="fa fa-fw fa-user"></i>Enrolled Beneficiaries
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="LblEnBenM" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-user"></i>Enrolled Male beneficiaries
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="LblEnBenF" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-user"></i>Enrolled Female beneficiaries
                                                                    </a>
                                                                </div>
                                                            </td>
                                                             <td width="100px"></td>
                                                        </tr>
                                                       <%-- <tr>
                                                            <td style="width: 395px">&nbsp;</td>
                                                            <td style="width: 30px">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>--%>
                                                    </table>

                                                    &nbsp;
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Chart ID="Chart1" runat="server" BackColor="0, 102, 204" BackGradientStyle="LeftRight"
                    BorderlineWidth="0" Height="200px" Palette="None" PaletteCustomColors="255, 102, 102"
                    Width="510px" BorderlineColor="102, 255, 102">
                    <Series>
                        <asp:Series Name="Series1" YValuesPerPoint="6">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <asp:Chart ID="Chart2" runat="server" BackColor="0, 102, 204" BackGradientStyle="LeftRight"
                    BorderlineWidth="0" Height="200px" Palette="None" PaletteCustomColors="153, 51, 255"
                    Width="510px" BorderlineColor="255, 204, 204">
                    <Series>
                        <asp:Series Name="Series2" YValuesPerPoint="6">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea2">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

