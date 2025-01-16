<%@ Page Title=":: :: SNSOP : : Work Progress " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="FundsUtilization" Codebehind="FundsUtilization.aspx.cs" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>   


      
<script>
    $(document).ready(function() {

        var rigtDynHt = $('.form-left-pan').height();
        //alert(rigtDynHt);
        $('.form-right-pan').css("height", rigtDynHt);

        $('#txtFundsUtilizationDate').datetimepicker();
    });
	</script>
    <script type="text/javascript" language="javascript">

function OpenPopup() 
 
   {

    window.open("Lookups/FundsUtilizationLookUp.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

    return false;
        }

        function ApplyChanges(val, val1) {

            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

            document.forms[0].submit();
        }
    </script>

    <script type="text/javascript">
    function inputOnlyNumbers(evt) {
        var e = window.event || evt; // for trans-browser compatibility  
        var charCode = e.which || e.keyCode;
        if ((charCode > 45 && charCode < 58) || charCode == 8) {
            return true;
        }
        return false;
    }
</script>

   <%-- <asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>--%>
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
                                <i class="fa fa-edit"></i> <a href="#">Funds Utilization</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">

                                <h3 class="panel-title">Funds Utilization</h3>
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
                                                        <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
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
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblCounty" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
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
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">Boma</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblBoma" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr  id="trMeeting" runat="server">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label408" runat="server" CssClass="LBLBLACK" Width="137px">Approved Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblWorkStartDate" runat="server" CssClass="LBLBLACK" 
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
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label420" runat="server" CssClass="LBLBLACK" Width="165px">Funds Utilization Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtFundsUtilizationDate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="10" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" 
                                                            PopupButtonID="txtFundsUtilizationDate" TargetControlID="txtFundsUtilizationDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                                                            ControlToValidate="txtFundsUtilizationDate" ErrorMessage="Please Enter Funds Utilization Date" 
                                                            SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                
                                                

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label435" runat="server" CssClass="LBLBLACK" Width="128px">Amount</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtAmount" runat="server" class="form-control txtbox"
                                                            Height="28px" TabIndex="5"  onkeypress="DecimalOnly()" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="txtAmount" 
                                                            ErrorMessage="Please Enter Amount" SetFocusOnError="True" 
                                                            ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                               <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label421" runat="server" CssClass="LBLBLACK" Width="165px">Funds Utilization for</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlFundsUtilizationFor" runat="server" class="form-control txtbox" 
                                                            Height="33px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Tools</asp:ListItem>
                                                            <asp:ListItem>Skilled labour</asp:ListItem>
                                                            <asp:ListItem>Materials</asp:ListItem>
                                                        </asp:DropDownList>
                                                    
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                                                            ControlToValidate="ddlFundsUtilizationFor" ErrorMessage="Please Select Funds Utilization" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&ensp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label422" runat="server" CssClass="LBLBLACK" Width="165px">Remarks</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtRemarks" runat="server" class="form-control txtbox" 
                                                            Height="60px" MaxLength="100" TabIndex="1" TextMode="MultiLine" 
                                                            ValidationGroup="group" Width="200px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                         </td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                      
                                                    </td>
                                                </tr>
                                               
                                                <%-- <tr >
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label425" runat="server" CssClass="LBLBLACK" Width="165px">Upload Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control txtbox" 
                                                            Height="28px" onchange="this.form.submit();" />
                                                        <asp:Label ID="lblFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
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
                                                        <asp:Image ID="Image1" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="195px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>--%>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
        
    </tr>

                 <tr>
                     <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnAdd_Click" TabIndex="10" Text="Add" 
                                                ValidationGroup="group" Width="90px" />
                         </td> 
                     </tr> 
                                    <tr>
                     <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                         <asp:GridView ID="grdList" runat ="server" 
                             AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" ForeColor="#333333" 
                                        GridLines="None" Height="62px" 
                                        PageSize="15" Width="100%">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                             <Columns >
                                           <asp:BoundField DataField="FundsUtilizationDate" DataFormatString ="{0:dd/MM/yyyy}" HeaderText="Date" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                            <asp:BoundField DataField="FundsUtilizationFor" HeaderText="Utilization For" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                 <asp:TemplateField >
                                     <ItemTemplate>
                                                    <asp:Button ID="btnDelete" class="fa fa-edit" OnClick ="btnDelete_Clicked" CommandArgument='<%#Eval("FundsUtilizationFor")%>' Text ="Delete" runat="server"/>
                                                </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                         </asp:GridView>
                                        </td> 
                                    </tr>                  
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            &nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Submit" 
                                                 Width="90px" />
                                            &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger" 
                                                Height="32px" onclick="BtnClear0_Click" TabIndex="10" Text="Cancel" 
                                                 Width="90px" CausesValidation="false"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <div ID="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" 
                                                    href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label 
                                                    ID="lblmsg" runat="server"></asp:Label>
                                            </div>
                                            
                                            <div ID="Failure" runat="server" class="alert alert-danger" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>
                                                Warning!</strong>
                                                <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile1" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group1" />
                            </div>
                        </div>
                    </div>
                </div>

    </div>
               
                 
         
                       
  <%--</ContentTemplate>
  </asp:UpdatePanel>--%>
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

