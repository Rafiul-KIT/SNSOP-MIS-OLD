<%@ Page Title=":: :: SNSOP : : Attendance Report" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="PDCHouseholdAttendanceRptLatestModel" Codebehind="PDCHouseholdAttendanceRptLatestModel.aspx.cs" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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
    <style>
.res {
            overflow-x:scroll;
            
}
.RadGrid {
    border-width: 0px;
    border-style: solid;
}
.MyGridHeader {
            background-color: #dedede !important;
            font-family: Poppins;
            vertical-align: middle;
            /*background-image:none !important;*/
        }

        .MyGridAlternatingRow {
            font-family:Poppins;
            background-color: #D1DDF1 !important;
        }
        .RadGrid_Default .rgRow>td
        {
            font-family:Poppins;
            background-color: #fff;
        }

        div.RadGrid_Default .rgHeader, div.RadGrid_Default th.rgResizeCol .HeaderTitle {
            background-image: none;
            color: #fff !important;
            font-size: 12px !important;
            font-weight: bold;
            font-family: Poppins;
            background-color: #013161 !important;
            vertical-align: top;
        }

            div.RadGrid_Default .rgHeader a {
                color: #fff !important;
                font-size: 12px !important;
                font-weight: bold;
                font-family: Poppins;
                vertical-align: top;
            }

    </style>
    <script type="text/javascript" language="javascript">

function OpenPopup() 
 
   {

       window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

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
    <asp:HiddenField ID="hdfID" runat="server" />

<div align="left">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="Home.aspx">Dashboard</a>
                            </li>
                            <li class="">
                                <i class="fa fa-fw fa-desktop"></i>  PDC
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Beneficiary Attendance Report</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Beneficiary Attendance Report</h3>
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
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblProject" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Work Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblWorkCode" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label417" runat="server" CssClass="LBLBLACK" Width="137px">Approved 
                                                        Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblWorkStartDate" runat="server" CssClass="LBLBLACK" 
                                                            Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label412" runat="server" CssClass="LBLBLACK" Width="128px">From Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                         <asp:TextBox ID="TxtFromDate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtStartDate2_CalendarExtender" runat="server" 
                                                            Format="dd-MM-yyyy" PopupButtonID="TxtFromDate" TargetControlID="TxtFromDate">
                                                        </cc1:CalendarExtender></td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                            ControlToValidate="TxtFromDate" ErrorMessage="Please Enter From Date" 
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label413" runat="server" CssClass="LBLBLACK" Width="128px">To Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                         <asp:TextBox ID="TxtToDate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                            Format="dd-MM-yyyy" PopupButtonID="TxtToDate" TargetControlID="TxtToDate">
                                                        </cc1:CalendarExtender></td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                            ControlToValidate="TxtToDate" ErrorMessage="Please Enter To Date" 
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
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
                                                        <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblState" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label428" runat="server" CssClass="LBLBLACK" Width="165px">County</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblCounty" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label429" runat="server" CssClass="LBLBLACK" Width="165px">Payam</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblPayam" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">Boma 
                                                        Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblBoma" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                               
                                              

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                       
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                         </td>
                                                </tr>
                                              
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 25px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="BtnSave3" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave3_Click" TabIndex="10" Text="Search" 
                                                ValidationGroup="group" Width="90px" />
                                            <asp:Button ID="BtnSave4" runat="server" CssClass="btn btn-danger" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Cancel" 
                                                ValidationGroup="group" Width="90px" />
                                            <asp:Button ID="btnexp" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-primary" Height="32px" 
                                                onclick="btnexp_Click" Text="Export" 
                                                ValidationGroup="vg" Width="90px" Visible="false" />
                                            <asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-danger" 
                                                Height="32px" onclick="BtnSave_Click" OnClientClick="return PrintPanel();" ToolTip="Export Excel Sheet" TabIndex="10" Text="Print" 
                                                ValidationGroup="group" Width="90px" />
                                        </td>
                                    </tr>
                                    
                                <div runat="server" id="div_print">
                                   <table runat="server" id="tblreport" class="table table-bordered table-responsive" BorderColor="#003399" BorderStyle="Solid" BorderWidth="1px" >
                                   <tr style="overflow-x: scroll; overflow-y: hidden;">
                                      <td colspan="3" style="max-width:750px">
                                       
                                        <asp:Panel ID="pnlContents" runat="server">
                                        <telerik:radgrid id="rgAttendanceReport" pagesize="250" PagerStyle-Font-Size="12px" autogeneratecolumns="true" runat="server" allowpaging="true"
                                        allowmultirowselection="true" allowsorting="true" gridlines="None" GridLinesVisibility="None"
                                        Font-Names="Poppins" ForeColor="#333333" 
                                        onneeddatasource="rgAttendanceReport_NeedDataSource" onitemdatabound="rgAttendanceReport_ItemDataBound" OnItemCommand="rgAttendanceReport_ItemCommand">
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                        <MasterTableView TableLayout="Auto" Width="100%"
                                        DataKeyNames="" 
                                        ClientDataKeyNames="" >                             
                                        <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>    
                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
               			         <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No" >
                                       <ItemTemplate>
                                         <asp:Label ID="rowNumberLabel" runat="server" Style="border-top-left-radius: 10px;" />
                                        </ItemTemplate>
                                            <HeaderStyle CssClass="rgHeader" BackColor="#013161" />
                                    </telerik:GridTemplateColumn>
                                    
               			         </Columns>
                            </MasterTableView>
                        </telerik:radgrid>                                               
                                                    </asp:Panel>

                                        
                                    </td>
                                    </tr>
                                    </table>
                                    <td colspan="3" style="padding: 15px; margin: 15px" valign="top">
                                            <asp:Label ID="lblmessage" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#CC3300"></asp:Label>
                                        </td>
                                    </div> 
                                    
                                </table>
                                
                                        
                                    
                                <asp:HiddenField ID="hdfcounts" runat="server" />
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

