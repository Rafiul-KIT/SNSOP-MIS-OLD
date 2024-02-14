<%@ Page Language="C#" MasterPageFile="CCMaster.master" AutoEventWireup="true" Inherits="DeleteRequestResult" Title=":: :: SSSNP : :: Home" Codebehind="DeleteRequestResult.aspx.cs" %>
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
    
    
   <div align="left">
        <ol class="breadcrumb">
            You are here &nbsp;!&nbsp; &nbsp; &nbsp;
            <li><i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a> </li> 
            <li class="active"><i class="fa fa-edit"></i><a href="#">Delete Result
                reject</a> </li>
        </ol>
    </div>
    <div align="center">
        <div class="row" align="center">
            <div class="col-lg-11">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Delete Result</h3>
                    </div>
                    <div class="panel-body">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;" valign="top" align="center">
                <br />
                <br />
                <asp:Label ID="Label1" runat="server" Text="Deleted Successfully" 
                    Font-Bold="True" Font-Size="16px" ForeColor="#006600"></asp:Label>
                <br />
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/UI/Public Works/frmBenDeletionNew.aspx?s=1"
                    Font-Bold="True" Font-Size="18px">Click here to Delete Another</asp:HyperLink>
       </td>
        </tr>
    </table>
    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

