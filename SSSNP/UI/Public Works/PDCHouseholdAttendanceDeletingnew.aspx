<%@ Page Title=":: :: SNSOP : : Attendance Report" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="PDCHouseholdAttendanceDeletingnew" Codebehind="PDCHouseholdAttendanceDeletingnew.aspx.cs" %>


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
                                <h3 class="panel-title">Beneficiary Attendance Deleting Report</h3>
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
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblBoma" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                              
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                       
                                                        <asp:Label ID="Label430" runat="server" CssClass="LBLBLACK" Width="165px">Enrollment No</asp:Label>
                                                       
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        
                                                        <asp:TextBox ID="TxtEnrolmentNo" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" 
                                                            Width="180px"></asp:TextBox>
                                                        
                                                        
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                             ControlToValidate="TxtEnrolmentNo" ErrorMessage="Please Enter Enrollment No" 
                                                             InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                         </td>
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                              <asp:GridView id="grdClosebatchdet" runat="server"
                                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" PageSize="50"
                                                        Width="100%" DataKeyNames="intBenid"
                                                        onrowdatabound="grdClosebatchdet_RowDataBound" 
                                                        onselectedindexchanged="grdClosebatchdet_SelectedIndexChanged" AllowPaging="True" 
                                                                    onpageindexchanging="grdClosebatchdet_PageIndexChanging">
                                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                                        <rowstyle backcolor="#FFFFFF" cssclass="GRDITEM" horizontalalign="Left" verticalalign="Middle" />
                                                        <columns>
                                                         <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <input ID="chkAll" runat="server" 
                                                                        onclick="javascript:SelectAllCheckboxes(this);" type="checkbox" />
                                                                    <asp:Label ID="Label10" runat="server" Text="All"></asp:Label>
                                                                </HeaderTemplate>
                                                                <%--<asp:TemplateField HeaderText="S No"></asp:TemplateField>--%>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                     <asp:HiddenField ID="hdfFlagID0" runat="server"/>
                                                                     <asp:HiddenField ID="hdfdate" runat="server"/>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                                <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                            </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="S.No">
                                                                                        <ItemTemplate>
                                                                                            <%# Container.DataItemIndex +1 %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:TemplateField>
</columns>
                                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" forecolor="White" />
                                                        <editrowstyle backcolor="#013161" />
                                                        <alternatingrowstyle backcolor="White" />
                                                    </asp:GridView>  
                                        </td>
                                    </tr>
                                   
                                   
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="lblStat" runat="server" Font-Bold="True" Font-Names="Verdana" 
                                                Font-Size="11px" ForeColor="Green" Style="position: static"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Button ID="BtnRemove" runat="server" CssClass="btn btn-danger" Height="30px" 
                                                OnClick="BtnRemove_Click" Text="DELETE" Width="80px" Visible="False" />
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
  </asp:UpdatePanel>
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

