<%@ Page Language="C#" Title=":: :: SNSOP : : Beneficiary Details " AutoEventWireup="true" MasterPageFile="~/UI/Public Works/LookupMaster.master" Inherits="frmBenPhotoDetails" Codebehind="frmBenPhotoDetails.aspx.cs" %>

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

       window.open("Lookups/BeneficiaryLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

    return false;
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


    function AlphaNumericOnly(evt) {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue >= 48 && AsciiValue <= 57))
            event.returnValue = true;
        else {
            event.returnValue = false;

            alert("Enter Alphabets,  and Characters  Only");
        }
    }
</script>

    <asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>

    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Photos Details</h3>
                            </div>
                            <div class="panel-body" >
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <%--<tr >
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="115px">Search</asp:Label>
                                            <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px" 
                                                onclick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False" 
                                                Font-Size="12px" Style="position: static" ToolTip="Rate Lookup" />
                                                  &nbsp;
                                                <div  >
                                                 
                                                  </div> 
                                        </td>
                                         <td runat="server" id="trlookup" style="padding: 1px; margin: 1px"  align="left">
                                          <a href="UploadsTrainee.aspx" class="btn btn-primary">Upload Beneficiary Details</a>
                                         </td>
                                        
                                    </tr>--%>
                                    <tr>
                                         
                                        <td style="padding: 5px; margin: 5px"  colspan="2" align="center">
                                            <%--<asp:Label ID="lblEnrollmentNo" Font-Bold="true" Visible="false" runat="server" CssClass="LBLBLACK" Width="365px"></asp:Label>--%>
                                        </td>
                                     
                                       
                                        
<td runat="server" id="Td1" style="padding: 1px; margin: 1px"  align="left">
                                          <%--<a href="UploadsTraineeUpdate.aspx" class="btn btn-primary">Upload Update Beneficiary Details</a>--%>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                              <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Font-Bold="true" Width="150px" Text="Principal Household"></asp:Label></td>
                                        <td style="width: 27px">
                                            <asp:Label ID="Label376" runat="server" CssClass="LBLBLACK" Width="165px">Beneficiary Photo</asp:Label>
                                        </td>
                                        <td valign="top">
                                            &nbsp;</td>
                                           
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                               
                                                

                                                
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="160px">Name of household head</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblhousehead" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>

                                           
                                               <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label13" runat="server" CssClass="LBLBLACK" Width="165px">Benfeciary Household Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                       <asp:Label ID="lblbenhhcode" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblgender" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                
                                                  <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label405" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblage" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                 
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                              <table cellpadding="4" cellspacing="5" style="width: 83%">
                                              
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image1" runat="server" Height="89px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="147px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>    
                                        </td>
                                        <td valign="top">
                                            
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td style="padding: 5px; margin: 5px; text-align: left" valign="top" 
                                            colspan="3">
                                            &nbsp;</td>
                                    </tr>
                                   
                                    <tr>
                                        <td colspan="3" 
                                            style="padding: 5px; margin: 5px; text-align: left;border-top-style: solid; border-top-width: 1px; border-color: #000000" 
                                            valign="top">
                                            &nbsp;</td>
                                    </tr>
                                   
                                    <tr>
                                        <td style="padding: 5px; margin: 5px; text-align: left;" valign="top">
                                            <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Font-Bold="true" 
                                                Text="Alternate one" Width="150px"></asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            <asp:Label ID="Label377" runat="server" CssClass="LBLBLACK" Font-Bold="true" 
                                                Text="Alternate two" Width="150px"></asp:Label>
                                        </td>
                                        <td 
                                            valign="top">
                                            <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Font-Bold="true" 
                                                Text="Alternate three" Width="150px"></asp:Label>
                                        </td>
                                    </tr>
                                   
             <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                              <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="137px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblaname1" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr> 
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label15" runat="server" CssClass="LBLBLACK" Width="128px">First Alternate HouseHold Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblahhcode1" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label18" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblagender1" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label12" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblaage1" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label24" runat="server" CssClass="LBLBLACK" Width="165px">First Alternate Payee Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                         <asp:Image ID="Image2" runat="server" Height="100px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                </table>
                                        </td>
                                        <td style="width: 27px">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%; vertical-align: top;">
                                               <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="137px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblaname2" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                      &nbsp;
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label16" runat="server" CssClass="LBLBLACK" Width="128px">Second Alternate HouseHold Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblahhcode2" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                  <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblagender2" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblaage2" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                  <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label26" runat="server" CssClass="LBLBLACK" Width="165px">Second Alternate Payee Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                         <asp:Image ID="Image3" runat="server" Height="100px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                 </table>  
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                               <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label19" runat="server" CssClass="LBLBLACK" Width="137px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="Label20" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                      &nbsp;
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label21" runat="server" CssClass="LBLBLACK" Width="128px">Third Alternate HouseHold Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label22" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                  <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label8" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="Label11" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                  <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label23" runat="server" CssClass="LBLBLACK" Width="165px">Third Alternate Payee Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                         <asp:Image ID="Image6" runat="server" Height="100px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="180px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                 </table>
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
                                            <div style="color:Red">
                                            <asp:Label ID="lblresult" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile1" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile2" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile3" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                            </div>
                        </div>
                    </div>
                </div>

    </div>
               
                 <%-- <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
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
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>


