<%@ Page Title=":: :: SNSOP : : Wages Payment Apply" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="ViewSelectionPayingagencyBenfWages" Codebehind="ViewSelectionPayingagencyBenfWages.aspx.cs" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
    
     <script type="text/javascript">
         //<![CDATA[
         function CheckAll(oCheckbox) {
             var GridView2 = document.getElementById("<%=grdDetails.ClientID %>");
             for (i = 1; i < GridView2.rows.length; i++) {
                 GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
             }
         }

         //]]>
 </script>

      
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

       window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

    return false;
    }
    </script>
    
    <script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnl1.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>Registration Certificate</title>');
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
    
    
 <script type="text/javascript">
     function PrintDiv() {
         var divContents = document.getElementById("dvContents").innerHTML;
         var printWindow = window.open('', '', 'height=600,width=900');
         printWindow.document.write('<html><head><title></title>');
         printWindow.document.write('</head><body >');
         printWindow.document.write(divContents);
         printWindow.document.write('</body></html>');
         printWindow.document.close();
         printWindow.print();
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
                                <i class="fa fa-fw fa-desktop"></i>  Wages Dashboard
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Selection by Paying agent</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">View Wages Payment Request Details</h3>
                            </div>
                            <div class="panel-body" >
                            
                              <asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-primary" 
                                    Height="32px" TabIndex="10" Text="Print" ValidationGroup="group" 
                                   Width="155px" OnClientClick = "return PrintPanel();" onclick="btnPrint_Click" />
                            <asp:Panel runat="server" ID="pnl1">
                             <div id="dvContents" style="border: 1px dotted black; padding: 5px;">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="lblWageRequestNo" runat="server" CssClass="LBLBLACK" 
                                                Width="400px" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                       :</td>
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
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
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
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
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
                                                        <asp:Label ID="Label430" runat="server" CssClass="LBLBLACK" Width="128px">Wage 
                                                        Rate(USD)</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblwagerate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                               
                                                
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label412" runat="server" CssClass="LBLBLACK" Width="128px">From Date</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                         :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblfromdate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                               
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label413" runat="server" CssClass="LBLBLACK" Width="128px">To Date</asp:Label>
                                                    </td>
                                                   <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                         :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lbltodate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
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
                                                        :</td>
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
                                                        :</td>
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
                                                        <asp:Label ID="Label432" runat="server" CssClass="LBLBLACK" Width="165px">Paying Agent</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblpayingagent" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label433" runat="server" CssClass="LBLBLACK" Width="165px">Co-Ordinator</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblcoordinator" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label434" runat="server" CssClass="LBLBLACK" Width="165px">Specialist</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblspecialist" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label435" runat="server" CssClass="LBLBLACK" Width="165px">Manager</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblmanager" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label431" runat="server" CssClass="LBLBLACK" Width="128px">Exchange Rate</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="txtExchangerate" runat="server" CssClass="LBLBLACK" 
                                                            Width="160px"></asp:Label>
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
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;" 
                                            valign="top" align="center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" CssClass="GRD" DataKeyNames="intBenid" ForeColor="#333333" 
                                                Height="62px" onrowdatabound="grdDetails_RowDataBound" PageSize="15" 
                                                ShowFooter="True" Width="100%">
                                                <footerstyle font-bold="True" forecolor="Black" />
                                                <rowstyle cssclass="GRDITEM" horizontalalign="Left" 
                                                    verticalalign="Middle" />
                                                <columns>
                                                 <asp:TemplateField HeaderText="All" Visible="False">
                                                        <HeaderTemplate>
                                                            <%-- <asp:CheckBox ID="CheckBox2" runat="server" />--%>
                                                            <asp:CheckBox ID="ChKAll" runat="server" __designer:wfdid="w5"  Checked="true"  Enabled="false"
                                                                AutoPostBack="True" onclick="CheckAll(this);" />
                                                            &nbsp;
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" __designer:wfdid="w4" Checked="true"  Enabled="false" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex +1 %>
                                                            
                                                            <asp:HiddenField ID="HdfReqID" runat="server" />
                                                               <asp:HiddenField ID="hdfRegBenid" runat="server" />
                                                               <asp:HiddenField ID="HdfBenid" runat="server" />
                                                                <asp:HiddenField ID="HdfBenWagesInvoiceid" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                                    <asp:BoundField DataField="NameofHousehold" HeaderText="Name of Household" />
                                                    <asp:BoundField DataField="ContactNo" HeaderText="PhoneNumber" />
                                                    <asp:BoundField DataField="cnt" HeaderText="No of Days Present" />
                                                    <%--DataField="Amt" --%>
                                                    <asp:BoundField   HeaderText="Amount Paid in USD"  />
                                                      <asp:BoundField   HeaderText="Amount Paid in SSP"  />
                                                       <asp:BoundField   HeaderText="Signature/Thumb Print"  
                                                        ItemStyle-Width="250px" ItemStyle-Height="100px" >
                                                      
                                                      
                                                           <ItemStyle Height="100px" Width="250px" />
                                                    </asp:BoundField>
                                                      
                                                      
                                                </columns>
                                                <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                                <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                                <headerstyle font-bold="True" 
                                                    forecolor="Black" />
                                                <editrowstyle backcolor="#B9D684" />
                                                <alternatingrowstyle backcolor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" 
                                            style="padding: 5px; margin: 5px; text-align: center;" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;" 
                                            valign="top" align="center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" 
                                            style="padding: 5px; margin: 5px; text-align: center;" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <div ID="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" 
                                                    href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label 
                                                    ID="lblmsg" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <div ID="Failure" runat="server" class="alert alert-danger" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>
                                                Warning!</strong>
                                                <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:HiddenField ID="hdfID" runat="server" />
                                            <asp:HiddenField ID="hdfFlagID" runat="server" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                        </td>
                                    </tr>
                                </table>
                                  
                                </div>
                              </asp:Panel>
                              
                              
              
                                <td style="width: 27px">
                                </td>
                                <td valign="top">
                                </td>
                                <tr>
                                    <td align="center" colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                                     </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 5px; margin: 5px; text-align: left;" 
                                        valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    </td>
                                </tr>
                              
                            </div>
                        </div>
                    </div>
                </div>

    </div>
               
                 
         
                       
  </ContentTemplate>
  </asp:UpdatePanel>
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

