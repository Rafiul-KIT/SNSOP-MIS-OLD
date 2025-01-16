<%@ Page Language="C#" Title=":: :: SNSOP : : Beneficiary Details " AutoEventWireup="true" MasterPageFile="~/UI/Public Works/CCMaster.master" Inherits="frmBenDeleteDetails" Codebehind="frmBenDeleteDetails.aspx.cs" %>

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
<div align="left">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="Home.aspx">Dashboard</a>
                            </li>
                            
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Deletion of Beneficiary</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Deletion of Beneficiary</h3>
                            </div>
                            <div class="panel-body" >
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
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
                                            </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
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
                                                        <asp:Label ID="Label359" runat="server" CssClass="LBLBLACK" Width="137px">Name of spouse</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblspouse" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                       &nbsp;
                                                    </td>
                                                </tr>
                                               <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label17" runat="server" CssClass="LBLBLACK" Width="165px">IP Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                       <asp:Label ID="lblipname" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                               <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="165px">Enrollment No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                       <asp:Label ID="lblenrollment" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
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
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">

                                              <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                       <asp:Label ID="lblstatename" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label373" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblcounty" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label404" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblpayamname" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                       <asp:Label ID="lblboma" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label376" runat="server" CssClass="LBLBLACK" Width="165px">Beneficiary Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image1" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            <asp:Label ID="Label407" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="244px">First Alternate Payee Details</asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top" align="left">
                                            <asp:Label ID="Label408" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="244px">Second Alternate Payee Details</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
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
                                                        <asp:Label ID="Label22" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblacontactno1" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
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
                                                         <asp:Image ID="Image2" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                
                                                </table></td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top" align="left">
                                             <table cellpadding="4" cellspacing="5" style="width: 83%">
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
                                                        <asp:Label ID="Label23" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblacontactno2" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
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
                                                         <asp:Image ID="Image3" runat="server" Height="153px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                 </table></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="165px">Work Code</asp:Label> &nbsp;&nbsp;
                                           <asp:GridView ID="grdDetails" runat="server"  
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" ForeColor="#333333" 
                                        GridLines="None" Height="62px"   Width="100%">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <%--<ItemStyle Width="50px" />--%>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WorkCode" HeaderText="Work Code" />
                                            <asp:BoundField DataField="" HeaderText="" />
                                            <asp:BoundField DataField="" HeaderText="" />
                                        </columns>
                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>
                                        </td>
                                        <td style="width: 50px"></td>
                                        <td valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="244px">Attendance</asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top" align="left">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                              <table cellpadding="4" cellspacing="5" style="width: 83%">
                                              <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK" Width="137px">Attendance</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblattendance" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>  
                                                
                                                
                                                </table>
                                                </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top" align="left">
                                                 &nbsp;</td>
                                    </tr> 
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top" colspan="3" Width="100%" >
                                            <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="165px">Wage Amounts</asp:Label> &nbsp;&nbsp;
                                           <asp:GridView ID="GridView1" runat="server"  
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD table-bordered " ForeColor="#333333" 
                                        GridLines="None" Height="62px"   Width="100%">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <%--<ItemStyle Width="50px" />--%>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WorkCode" HeaderText="Work Code" ItemStyle-Width="220px" />
                                            <asp:BoundField DataField="RequestNo" HeaderText="Request No" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                            <asp:BoundField DataField="TotalWorkDays" HeaderText="Total Work Days" />
                                            <asp:BoundField DataField="WageRateUSDTotal" HeaderText="Total WageRateUSD" />
                                            <asp:BoundField DataField="ExchangeRate" HeaderText="Exchange Rate" />
                                        </columns>
                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>
                                        </td>
                                        <%--<td style="width: 50px"></td>
                                        <td valign="top">
                                            &nbsp;</td>--%>
                                    </tr>
                                     <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top" colspan="3" Width="100%" >
                                            <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="165px">Paid Amounts</asp:Label> &nbsp;&nbsp;
                                           <asp:GridView ID="GridView2" runat="server"  
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD table-bordered " ForeColor="#333333" 
                                        GridLines="None" Height="62px"   Width="100%">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <%--<ItemStyle Width="50px" />--%>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WorkCode" HeaderText="Work Code" ItemStyle-Width="170px" />
                                            <asp:BoundField DataField="RequestNo" HeaderText="Request No" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                            <asp:BoundField DataField="TotalWorkDays" HeaderText="Total Work Days" />
                                            <asp:BoundField DataField="WageRateUSDTotal" HeaderText="Total WageRateUSD" />
                                            <asp:BoundField DataField="ExchangeRate" HeaderText="Exchange Rate" />
                                            <asp:BoundField DataField="PayStatus" HeaderText="Pay Status" />
                                            <asp:BoundField DataField="Paidto" HeaderText="Paid To" />
                                            <asp:BoundField DataField="PaidToType" HeaderText="Paid To Type" />
                                            <asp:BoundField DataField="PaidToName" HeaderText="Paid To Name" ItemStyle-Width="150px"  />
                                        </columns>
                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" Height="60px" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>
                                        </td>
                                        <%--<td style="width: 50px"></td>
                                        <td valign="top">
                                            &nbsp;</td>--%>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3" style="padding: 5px; margin: 5px" valign="top" 
                                            Width="100%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3" 
                                            style="padding: 5px; margin: 5px; text-align: center;" valign="top" 
                                            Width="100%">
                                            <asp:Button ID="BtnDelete" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-warning" Height="32px" onclick="BtnDelete_Click" 
                                                OnClientClick="return confirm('Do you want to delete the record ? ');" 
                                                TabIndex="10" Text="Delete" Visible="false" Width="90px" />
                                            &nbsp;&nbsp;<asp:Button ID="BtnDeleteTransaction" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-warning" Height="32px" onclick="BtnDeleteTransaction_Click" 
                                                OnClientClick="return confirm('Do you want to delete the transaction record and Please copy transaction before delete? ');" 
                                                TabIndex="10" Text="Delete Unpaid Transaction" Visible="false" Width="200px" />
                                            &nbsp;&nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-danger" Height="32px" onclick="BtnClear_Click" TabIndex="10" 
                                                Text="Back" ToolTip="To Clear  the Screen" Width="90px" />
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


