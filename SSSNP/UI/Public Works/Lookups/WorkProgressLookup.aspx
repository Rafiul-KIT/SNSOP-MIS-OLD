<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/Lookups/CCLookupsMaster.master" AutoEventWireup="true" Inherits="WorkProgressLookup" Title=":: SSSNP Online Management System ::"  EnableEventValidation="false" Codebehind="WorkProgressLookup.aspx.cs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <%-- <script type="text/javascript">
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
<style>
  .td {
   padding: 5px;
   }
</style>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; font-family:Poppins" >
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            &nbsp;Lookup</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%" 
                                                __designer:mapid="17eb">
                                                <tr __designer:mapid="17ec">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                        <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                        <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px" ValidationGroup="group">
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
                                                        <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">County Name</asp:Label>
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

                                                 <tr style="display:none;">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="124px">Group Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                        <asp:DropDownList ID="ddl_group_type" runat="server" class="form-control txtbox" 
                                                            Height="33px" AutoPostBack="True" 
                                                            >
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
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="124px">Payam Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1804">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="1805">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" 
                                                            Height="33px" Width="180px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlPayam_SelectedIndexChanged">
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
                                                        <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Work Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                        <asp:TextBox ID="txtWorkcode" runat="server" class="form-control txtbox" 
                                                            Height="33px" MaxLength="40"  TabIndex="1" 
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
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="Poppins" 
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
                                        GridLines="None" Height="62px" Font-Names="Poppins"
                                        onpageindexchanging="grdDetails_PageIndexChanging" 
                                        onrowdatabound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                        <footerstyle backcolor="#013161" Font-Names="Poppins" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" Font-Names="Poppins" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="Poppins">Select</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                            <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                            <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                            <asp:BoundField DataField="PrjName" HeaderText="Project Name" />                                            
                                            <asp:BoundField DataField="IPName" HeaderText="IP Name" /> 
                                            <asp:BoundField DataField="WorkCode" HeaderText="Work Code" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                                
                                        </columns>
                                        <pagerstyle backcolor="#013161" Font-Names="Poppins"  forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" Font-Names="Poppins" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" Font-Names="Poppins"  />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>
        </asp:Panel>
                                         </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>

