<%@ Page Title=":: :: SNSOP : : Wages Report from Pay Connect Team" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="WageAbstractReportsAPI" Codebehind="WageAbstractReportsAPI.aspx.cs" %>


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
	<script type = "text/javascript">
	    function PrintPanel() {
	        var panel = document.getElementById("<%=pnlContents.ClientID %>");
	        var printWindow = window.open('', '', 'height=400,width=800');
	        printWindow.document.write('<html><head><title>DIV Contents</title>');
	        printWindow.document.write('</head><body >');
	        printWindow.document.write(panel.innerHTML);
	        printWindow.document.write('</body></html>');
	        printWindow.document.close();
	        setTimeout(function() {
	            printWindow.print();
	        }, 500);
	        return false;
	    }
    </script>
    <script type="text/javascript" language="javascript">

function OpenPopup() 
 
   {

       window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

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
                                <i class="fa fa-fw fa-desktop"></i>  
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Wages Paid Abstract from Pay Connect API</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Wages Paid Abstract from Pay Connect API </h3>
                            </div>
                            <div class="panel-body" >
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label443" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
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
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label373" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" AutoPostBack="True" 
                                                            class="form-control txtbox" Height="33px" 
                                                            onselectedindexchanged="ddlCounties_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label404" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" AutoPostBack="True" 
                                                            class="form-control txtbox" Height="28px" 
                                                            onselectedindexchanged="ddlPayam_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
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
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox" 
                                                            Height="33px" ValidationGroup="group" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Work Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtWorkcode" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                 
                                              

                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="BtnSave3" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave3_Click" TabIndex="10" Text="Search" 
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnSave4" runat="server" CssClass="btn btn-danger" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Cancel" 
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="btnexp" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-primary" Font-Size="12px" Height="32px" 
                                                onclick="btnexp_Click" Style="position: static" Text="Export" 
                                                ValidationGroup="vg" />
                                            &nbsp;<asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-danger" 
                                                Height="32px" onclick="BtnSave_Click" OnClientClick="return PrintPanel();" ToolTip="Export Excel Sheet" TabIndex="10" Text="Print" 
                                                ValidationGroup="group" Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                        <div runat="server" id="div_print">
   <asp:Panel ID="pnlContents" runat="server">
   
   
                                              
   
   <table runat="server" id="tblreport" visible="false" style="padding: 5px"><tr>
       <td>
       
         <asp:Label ID="LblResultA" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#CC3300"></asp:Label>
                                                
                                                  <br />
           <asp:Label ID="LblResultA0" runat="server" Font-Bold="True" Font-Size="12px" 
               ForeColor="#CC3300"></asp:Label>
                                                
                                                  <br />
                                                  
                                                  
           <asp:GridView ID="grdClosebatchdet" runat="server" CellPadding="4" 
               CssClass="GRD" ForeColor="#333333" 
               onpageindexchanging="grdClosebatchdet_PageIndexChanging" 
               onrowdatabound="grdClosebatchdet_RowDataBound" 
               onselectedindexchanged="grdClosebatchdet_SelectedIndexChanged" PageSize="50" 
               Width="100%" OnPreRender="grdClosebatchdet_PreRender">
               <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
               <rowstyle backcolor="#FFFFFF" cssclass="GRDITEM" horizontalalign="Left" 
                   verticalalign="Middle" />
               <columns>
                   <asp:TemplateField HeaderText="S.No">
                       <ItemTemplate>
                           <%# Container.DataItemIndex +1 %>
                       </ItemTemplate>
                       <HeaderStyle HorizontalAlign="Left" />
                       <ItemStyle Width="30px" />
                   </asp:TemplateField>
               </columns>
               <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
               <EmptyDataTemplate>
                   There is no attendance for this work in&nbsp; these days
               </EmptyDataTemplate>
               <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
               <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                   forecolor="White" />
               <editrowstyle backcolor="#013161" />
               <alternatingrowstyle backcolor="White" />
           </asp:GridView>
       </td></tr>
   
   
       </table>
                                             
                                                    </asp:Panel>
                                    </div> 
                                        </td>
                                    </tr>
                                   
                                   
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="lblmessage" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#CC3300"></asp:Label>
                                        </td>
                                    </tr>
                                   
                                   
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                            </div>
                        </div>
                    </div>
                </div>

    </div>
               
                 
         
                       
  </ContentTemplate>
  <Triggers>
            <asp:PostBackTrigger ControlID="btnexp" />
            </Triggers>
  
  </asp:UpdatePanel>
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

