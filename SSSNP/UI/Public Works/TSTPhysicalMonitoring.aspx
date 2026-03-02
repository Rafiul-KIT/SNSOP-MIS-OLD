<%@ Page Title=":: :: SNSOP : : Physical Monitoring " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="TSTPhysicalMonitoring" Codebehind="TSTPhysicalMonitoring.aspx.cs" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../Resource/js/jquery.js" type="text/javascript"></script>
    <script src="../Resource/js/bootstrap.min.js"></script>   
    <script src="../Resource/js/plugins/morris/raphael.min.js"></script>
    <script src="../Resource/js/plugins/morris/morris.min.js"></script>
    <script src="../Resource/js/plugins/morris/morris-data.js"></script>
    
    <script src="../../Resource/js/bootstrap-datetimepicker.js" type="text/javascript"></script>
     
<script>
    $(document).ready(function() {

        var rigtDynHt = $('.form-left-pan').height();
        //alert(rigtDynHt);
        $('.form-right-pan').css("height", rigtDynHt);

        $('#txtStartDate').datetimepicker();
    });
	</script>
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
<div align="left">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="Home.aspx">Dashboard</a>
                            </li>
                            <li class="">
                                <i class="fa fa-fw fa-desktop"></i>  TST
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Field Monitoring</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Field Monitoring</h3>
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
                                                        <asp:Label ID="Label419" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblProject" runat="server" CssClass="LBLBLACK" Width="165px" 
                                                            Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Work 
                                                        Code</asp:Label>
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
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        &nbsp;</td>
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
                                                        <asp:Label ID="Label420" runat="server" CssClass="LBLBLACK" Width="165px">Boma</asp:Label>
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
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:GridView ID="gvpractical0" runat="server" AutoGenerateColumns="False" 
                                                BorderColor="#003399" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                                CssClass="GRD" DataKeyNames="intWorkid,intWorkActivityId" Font-Names="Verdana" 
                                                Font-Size="12px" ForeColor="#333333" GridLines="None" Width="100%">
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#FFFFFF" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex +1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="WorkName" HeaderText="Work Name" />
                                                    <asp:BoundField DataField="AreaofWork" HeaderText="Area of Work" />
                                                    <asp:BoundField DataField="WorkActivity" HeaderText="Work Activity" />
                                                    <asp:BoundField DataField="EstManPower" HeaderText="Estimated Manpower" />
                                                    <asp:BoundField DataField="EstDays" HeaderText="Estimated Days" 
                                                        Visible="False" />
                                                    <asp:BoundField DataField="EstEquipmnts" 
                                                        HeaderText="Estimated Equipment Required" />
                                                    <asp:BoundField DataField="EstMaterial" 
                                                        HeaderText="Estimated Material Required" />
                                                    <asp:BoundField DataField="EstSkilLabour" 
                                                        HeaderText="Estimated Skill Labour Required" />
                                                    <asp:BoundField DataField="EstWorkStartDate" DataFormatString="{0:MM-dd-yyyy}" 
                                                        HeaderText="Estimated Work Start Date" />
                                                    <asp:BoundField DataField="EstWorkEndDate" DataFormatString="{0:MM-dd-yyyy}" 
                                                        HeaderText="Estimated Work End Date" />
                                                    <asp:BoundField DataField="EstCost" HeaderText="Estimated Cost" />
                                                </Columns>
                                                <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#013161" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label423" runat="server" CssClass="LBLBLACK" Width="165px">Date of Visit</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="11" onkeypress="Dates()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" 
                                                            PopupButtonID="txtStartDate" TargetControlID="txtStartDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                                                            ControlToValidate="txtStartDate" ErrorMessage="Please Enter Date of Visit" 
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label424" runat="server" CssClass="LBLBLACK" Width="165px">Out come</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlOutcome" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Satisfied</asp:ListItem>
                                                            <asp:ListItem>Not Satisfied</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                                            ControlToValidate="ddlOutcome" ErrorMessage="Please Select Outcome" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px" valign="top">
                                                        <asp:Label ID="Label426" runat="server" CssClass="LBLBLACK" Width="165px">Remarks</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtRemarks" runat="server" class="form-control txtbox" 
                                                            Height="70px" MaxLength="100" onkeypress="Names()" TabIndex="1" 
                                                            TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="6" cellspacing="10" style="width: 100%">
                                                <tr>
                                                    <td style="width: 289px">
                                                        <asp:Label ID="Label376" runat="server" CssClass="LBLBLACK" Width="156px">Photo 
                                                        1</asp:Label>
                                                    </td>
                                                    <td style="width: 295px">
                                                        <asp:Label ID="Label410" runat="server" CssClass="LBLBLACK" Width="156px">Photo 
                                                        2 </asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label411" runat="server" CssClass="LBLBLACK" Width="156px">Photo 
                                                        3</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 289px">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control txtbox" Width="200px" 
                                                            Height="28px" onchange="this.form.submit();" />
                                                    </td>
                                                    <td style="width: 295px">
                                                        <asp:FileUpload ID="FileUpload2" runat="server" class="form-control txtbox" Width="200px" 
                                                            Height="28px" onchange="this.form.submit();" />
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="FileUpload3" runat="server" class="form-control txtbox" Width="200px" 
                                                            Height="28px" onchange="this.form.submit();" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 289px">
                                                        <asp:Image ID="Image1" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                        <asp:Label ID="lblFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="width: 295px">
                                                        <asp:Image ID="Image2" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                        <asp:Label ID="lblFileName2" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="Image3" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                        <br />
                                                        <asp:Label ID="lblFileName3" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Submit" 
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Cancel" 
                                                ValidationGroup="group" Width="90px" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile1" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile2" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile3" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                
                                <div ID="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" 
                                                    href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label 
                                                    ID="lblmsg" runat="server"></asp:Label>
                                            </div>
                            </div>
                        </div>
                    </div>
                </div>

    </div>
               
                 
          <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
<ProgressTemplate>
<div class="overlay">
<%--<div style=" z-index: 1000; margin-left: 350px;margin-top:200px;opacity: 1;-moz-opacity: 1;">--%>
<div style=" z-index: 1000; margin-left: -210px;margin-top:100px;opacity: 1; -moz-opacity: 1;">
<img alt="" src="../../Resource/Images/Processing.gif" />

</div>
    
</div>
</ProgressTemplate>
</asp:UpdateProgress>

                       
  </ContentTemplate>
  </asp:UpdatePanel>
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

