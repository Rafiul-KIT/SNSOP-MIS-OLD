<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/Lookups/CCLookupsMaster.master" AutoEventWireup="true" Inherits="BeneficiaryLookup" Title=":: SSSNP Online Management System ::" EnableEventValidation="false" Codebehind="BeneficiaryLookup.aspx.cs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--<script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
	        var printWindow = window.open('', '', 'height=400,width=800');
	        printWindow.document.write('<html><head><title>DIV Contents</title>');
	        printWindow.document.write('</head><body >');
	        printWindow.document.write(panel.innerHTML);
	        printWindow.document.write('</body></html>');
	        printWindow.document.close();
	        setTimeout(function () {
	            printWindow.print();
	        }, 500);
	        return false;
        }
    </script>--%>
<script type="text/javascript">
   function GetRowValue(val)
    {   
    if(val != '&nbsp;')
    {   
        val1 = 0;
        window.opener.ApplyChanges(val, val1);

    //window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfID').value = val;
    //window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfFlagID').value = val1;

    }
    //window.opener.document.forms[0].submit();
    self.close();
   
    }
    
   </script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            &nbsp;Beneficiary Enrollment Lookup</h3>
                    </div>
                    <div class="panel-body">
                   <%--  <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>--%>
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%" 
                                                __designer:mapid="17eb">
                                                <tr __designer:mapid="17ec">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr __designer:mapid="17f5">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                        <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox" 
                                                            Height="33px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlCounties_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr __designer:mapid="17f5">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                        <asp:Label ID="Label402" runat="server" CssClass="LBLBLACK" Width="144px">Enrollment Number</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                        <asp:TextBox ID="txtEnrollment" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            
                                                            
                                                            
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">
                                                        &nbsp;</td>
                                                </tr>

                                                <tr >
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="124px">Group Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                        <asp:DropDownList ID="ddl_group_type" runat="server" class="form-control txtbox" 
                                                            Height="33px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddl_group_type_SelectedIndexChanged">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                             <asp:ListItem Value="Group">Group</asp:ListItem>
                                                             <asp:ListItem Value="CST">CST</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                </td>
                                <td style="width: 27px">
                                    <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                </td>
                                <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%" 
                                                __designer:mapid="1800">
                                                <tr __designer:mapid="1801">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1802">
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="124px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1804">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1805">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1809">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr __designer:mapid="180a">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                        <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Width="144px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr __designer:mapid="180a">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                        <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                        <asp:TextBox ID="txtName" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40"  TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            
                                                            
                                                            
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                        &nbsp;</td>
                                                </tr>

                                                  <tr __designer:mapid="180a" runat="server" id="tr_group_code" visible="false">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                        <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="144px">Group Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                        <asp:DropDownList ID="ddl_group_code" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px" AutoPostBack="True">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                              
                                                             <asp:ListItem Value="01">01</asp:ListItem>
                                                             <asp:ListItem Value="02">02</asp:ListItem>
                                                             <asp:ListItem Value="03">03</asp:ListItem>
                                                             <asp:ListItem Value="04">04</asp:ListItem>
                                                             <asp:ListItem Value="05">05</asp:ListItem>
                                                             <asp:ListItem Value="06">06</asp:ListItem>
                                                             <asp:ListItem Value="07">07</asp:ListItem>
                                                             <asp:ListItem Value="08">08</asp:ListItem>
                                                             <asp:ListItem Value="09">09</asp:ListItem>
                                                             <asp:ListItem Value="10">10</asp:ListItem>
                                                             <asp:ListItem Value="11">11</asp:ListItem>
                                                             <asp:ListItem Value="12">12</asp:ListItem>
                                                             <asp:ListItem Value="13">13</asp:ListItem>
                                                             <asp:ListItem Value="14">14</asp:ListItem>
                                                             <asp:ListItem Value="15">15</asp:ListItem>
                                                             <asp:ListItem Value="16">16</asp:ListItem>
                                                             <asp:ListItem Value="17">17</asp:ListItem>
                                                             <asp:ListItem Value="18">18</asp:ListItem>
                                                             <asp:ListItem Value="19">19</asp:ListItem>
                                                             <asp:ListItem Value="20">20</asp:ListItem>
                                                             <asp:ListItem Value="21">21</asp:ListItem>
                                                             <asp:ListItem Value="22">22</asp:ListItem>
                                                             <asp:ListItem Value="23">23</asp:ListItem>
                                                             <asp:ListItem Value="24">24</asp:ListItem>
                                                             <asp:ListItem Value="25">25</asp:ListItem>
                                                             <asp:ListItem Value="26">26</asp:ListItem>
                                                             <asp:ListItem Value="27">27</asp:ListItem>
                                                             <asp:ListItem Value="28">28</asp:ListItem>
                                                             <asp:ListItem Value="29">29</asp:ListItem>
                                                             <asp:ListItem Value="30">30</asp:ListItem>
                                                             <asp:ListItem Value="31">31</asp:ListItem>
                                                             <asp:ListItem Value="32">32</asp:ListItem>
                                                             <asp:ListItem Value="33">33</asp:ListItem>
                                                             <asp:ListItem Value="34">34</asp:ListItem>
                                                             <asp:ListItem Value="35">35</asp:ListItem>
                                                             <asp:ListItem Value="36">36</asp:ListItem>
                                                             <asp:ListItem Value="37">37</asp:ListItem>
                                                             <asp:ListItem Value="38">38</asp:ListItem>
                                                             <asp:ListItem Value="39">39</asp:ListItem>
                                                             <asp:ListItem Value="40">40</asp:ListItem>
                                                             <asp:ListItem Value="41">41</asp:ListItem>
                                                             <asp:ListItem Value="42">42</asp:ListItem>
                                                             <asp:ListItem Value="43">43</asp:ListItem>
                                                             <asp:ListItem Value="44">44</asp:ListItem>
                                                             <asp:ListItem Value="45">45</asp:ListItem>
                                                             <asp:ListItem Value="46">46</asp:ListItem>
                                                             <asp:ListItem Value="47">47</asp:ListItem>
                                                             <asp:ListItem Value="48">48</asp:ListItem>
                                                             <asp:ListItem Value="49">49</asp:ListItem>
                                                             <asp:ListItem Value="50">50</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                            </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" 
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                        Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Search" 
                                        Width="90px" />
                                    <asp:Button ID="BtnClear" runat="server" CausesValidation="False" 
                                        CssClass="btn btn-danger" Height="32px" onclick="BtnClear_Click" TabIndex="10" 
                                        Text="Cancel" ToolTip="To Clear  the Screen" Width="90px" />

                                    <asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-warning" Visible="false" 
                                                Height="32px"  OnClick="PrintAllPages" ToolTip="Print" TabIndex="10" Text="Print" 
                                                ValidationGroup="group" Width="90px" />

                                     <asp:Button ID="btnExportoexcel" runat="server" CssClass="btn btn-success" Visible="false"  
                                                Height="32px"  onclick="btnExportoexcel_Click" ToolTip="Export To Excel" TabIndex="10" Text="Export" 
                                                ValidationGroup="group" Width="90px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                     <div runat="server" id="div_print">
   <asp:Panel ID="pnlContents" runat="server">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" ForeColor="#333333" 
                                        GridLines="None" Height="62px" 
                                        onpageindexchanging="grdDetails_PageIndexChanging" 
                                        onrowdatabound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" class="fa fa-edit" runat="server">Select</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
<asp:BoundField DataField="WorkCode" HeaderText="Work Code" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                            <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                            <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                            <asp:BoundField DataField="NameofHousehold" 
                                                HeaderText="Name" />
                                            <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                             <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                            <asp:BoundField DataField="GroupId" HeaderText="Group Code" />
                                        </columns>
                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>
                                    <asp:GridView ID="grdDetailsExport" runat="server" 
                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" 
                                        GridLines="None" Height="62px" 
                                        
                                         PageSize="15" Width="100%">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                            <columns>
                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                                    <ItemTemplate >
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="50px" />
                                                                    
                                                                </asp:TemplateField>
                                                                </columns>
                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>
        </asp:Panel>
                                         </div>
                                </td>
                            </tr>
                        </table>
                       <%-- </ContentTemplate>
                          <Triggers>
            <asp:PostBackTrigger ControlID="BtnSave1" />
            <asp:PostBackTrigger ControlID="BtnPrint" />
            <asp:PostBackTrigger ControlID="btnExportoexcel" />
        </Triggers>
                        
                        
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>

