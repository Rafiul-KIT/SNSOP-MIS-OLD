<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="frmAlternateExistUpdrpt" Title=":: SSSNP Online Management System ::" EnableEventValidation="false" Codebehind="frmAlternateExistUpdrpt.aspx.cs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
   function GetRowValue(val,valinvoice)
    {   
    if(val != '&nbsp;')
    {   
    val1 = 0;
    window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfID').value = val;
    window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfFlagID').value = val1;
    window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfInVoiceID').value = valinvoice;

    }
    window.opener.document.forms[0].submit();
    self.close();
   
    }
    
   </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="h3" runat ="server">
                          Alternate Existing Updations Report </h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            
                            <tr>
                                
                                <td align="center" style="padding: 5px; margin: 5px">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 60%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top" >
                                            <table cellpadding="4" cellspacing="5" style="width: 83%" 
                                                __designer:mapid="17eb">
                                                <tr __designer:mapid="17ec">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                        <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                        <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px" ValidationGroup="group"
                                                            onselectedindexchanged="ddlIP_SelectedIndexChanged" AutoPostBack="true" >
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr __designer:mapid="17ec">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px" 
                                                            onselectedindexchanged="ddlState_SelectedIndexChanged" AutoPostBack="True">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr __designer:mapid="17f5">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                        <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">County Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox" 
                                                            Height="33px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlCounties_SelectedIndexChanged1" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="124px">Beneficiary Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtbenf" runat="server" class="form-control txtbox" Height="33px"
                                                                MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                                                                
                                            </table>
                                </td>
                               <%-- <td style="width: 27px">
                                    <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                </td>--%>
                                <td  valign="top"  >
                                            <table cellpadding="4" cellspacing="5" style="width: 83%" 
                                                __designer:mapid="1800">
                                                <tr __designer:mapid="1801">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1802">
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="124px">Payam Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1804">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1805">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlPayam_SelectedIndexChanged1">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1809">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr __designer:mapid="180a">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                        <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Width="144px">Boma Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="124px">Household No</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txthousehold" runat="server" class="form-control txtbox" Height="55px"
                                                                MaxLength="100" TabIndex="1" TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="124px">Enrollment No</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtenrollment" runat="server" class="form-control txtbox" Height="33px"
                                                                MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                <tr __designer:mapid="180a" runat="server" visible="false" >
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                        <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Work Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                        <asp:TextBox ID="txtWorkcode" runat="server" class="form-control txtbox" 
                                                            Height="33px" MaxLength="140"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            
                                                            
                                                            
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                        Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Search" 
                                        Width="90px" />
                                    &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False" 
                                        CssClass="btn btn-danger" Height="32px" onclick="BtnClear_Click" TabIndex="10" 
                                        Text="Cancel" ToolTip="To Clear  the Screen" Width="90px" />
                                    &nbsp;<asp:Button ID="btnexp" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-primary" Font-Size="12px" Height="32px" 
                                                onclick="btnexp_Click" Style="position: static" Text="Export" Width="90px"
                                                ValidationGroup="vg" Visible="false" /></td>
                            </tr>
                            </table>
                                </td>
                            </tr>
                            
                            <tr>
                                
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" 
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>
                                        
                                         <asp:Label ID="lblresult" runat="server" Font-Bold="True" Font-Names="verdana" 
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px" width="50%">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD table-bordered " ForeColor="#333333" 
                                        GridLines="None" Height="62px" 
                                        onpageindexchanging="grdDetails_PageIndexChanging" 
                                        onrowdatabound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                        
                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex +1 %>
                                                            
                                                            <asp:HiddenField ID="hdfid" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                            
                                              <asp:BoundField DataField="IPName" HeaderText="Name of the IP"></asp:BoundField>
                                              <asp:BoundField DataField="StateName" HeaderText="State Name"></asp:BoundField>
                                              <asp:BoundField DataField="CountieName" HeaderText="County Name"></asp:BoundField>
                                              <asp:BoundField DataField="PayamName" HeaderText="Payam Name"></asp:BoundField>
                                              <asp:BoundField DataField="BomaName" HeaderText="Boma Name"></asp:BoundField>
                                              <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                              <asp:BoundField DataField="NameofHousehold" HeaderText="Name of the Household" />
                                              <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                              <asp:BoundField DataField="household_number" HeaderText="HouseHold Number" />
                                              <asp:BoundField DataField="AlternateType" HeaderText="Alternate Type" />
                                              <asp:BoundField DataField="alternate_number" HeaderText="Alternate Number" />
                                              <asp:BoundField DataField="NameAlternatePayee" HeaderText="Name of the Alternate Payee" />
                                              <asp:BoundField DataField="AlternatePayeeGender" HeaderText="Alternate Payee Gender" />
                                              <asp:BoundField DataField="AlternatePayeeAge" HeaderText="Alternate Payee Age" />
                                              <asp:BoundField DataField="AlternatePayeeNationID" HeaderText="Alternate Payee Nation ID" />
                                              <asp:BoundField DataField="AlternatePayeeMobile" HeaderText="Alternate Payee Mobile No" />
                                              <asp:BoundField DataField="ModifiedDate" HeaderText="Modified Date" DataFormatString="{0:dd MMM yyyy}" />
                                         
                                        </columns>
                                        <RowStyle CssClass="GridviewScrollC1Item" />
                                        <PagerStyle CssClass="GridviewScrollC1Pager" />
                                        <SelectedRowStyle CssClass="GridviewScrollC1Item2" />
                                        <HeaderStyle CssClass="GridviewScrollC1Header" />
                                        <FooterStyle CssClass="GridviewScrollC1Footer" />
                                        <EditRowStyle CssClass="GridviewScrollC1Item2" />
                                        <AlternatingRowStyle CssClass="GridviewScrollC1Item2" />
                                    </asp:GridView>
                                     <asp:HiddenField  ID="hdnStat" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>

