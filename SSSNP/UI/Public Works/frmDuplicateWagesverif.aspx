<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="frmDuplicateWagesverif" Title=":: SSSNP Online Management System ::" EnableEventValidation="false" Codebehind="frmDuplicateWagesverif.aspx.cs" %>

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
                           Duplicate Wages Request Verification</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            
                            <tr>
                                
                                <td align="center" style="padding: 5px; margin: 5px">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                           
                          <%--  <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    &nbsp;</td>
                            </tr>--%>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                        Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Search" 
                                        Width="90px" Visible= "false" />
                                    &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False" 
                                        CssClass="btn btn-danger" Height="32px" onclick="BtnClear_Click" TabIndex="10" 
                                        Text="Cancel" ToolTip="To Clear  the Screen" Width="90px" Visible= "false" />
                                    &nbsp;<asp:Button ID="btnexp" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-primary" Font-Size="12px" Height="32px" 
                                                onclick="btnexp_Click" Style="position: static" Text="Export" Width="90px"
                                                ValidationGroup="vg" Visible= "false" /></td>
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
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD table-bordered " ForeColor="#333333" 
                                        GridLines="None" Height="62px" ShowFooter="true"
                                        onpageindexchanging="grdDetails_PageIndexChanging" 
                                        onrowdatabound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White"/>
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Center" 
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
                                                    <asp:BoundField DataField="intWorkid" HeaderText="Work ID" />
                                              <asp:BoundField DataField="ReqNo" HeaderText="Registered No" />
                                              <asp:BoundField DataField="ReqFromDate" HeaderText="Request From Date" DataFormatString="{0:dd MMM yyyy}"/>
                                              <asp:BoundField DataField="ReqToDate" HeaderText="Request To Date" DataFormatString="{0:dd MMM yyyy}" />
                                              <%--<asp:BoundField DataField="Status" HeaderText="Status" />--%>
                                              <asp:HyperLinkField  DataTextField="Total" HeaderText ="Allocated Benfeciaries" ItemStyle-HorizontalAlign ="Right"/>
                                              <asp:HyperLinkField  DataTextField="cnt" HeaderText ="Applied Benfeciaries" ItemStyle-HorizontalAlign ="Right"/>
                                         
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

