<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="ManagerInvoices" Title=":: SSSNP Online Management System ::" Codebehind="ManagerInvoices.aspx.cs" %>

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
                            </h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            
                            <tr>
                                
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" 
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD table-bordered " ForeColor="#333333" 
                                        GridLines="None" Height="62px" 
                                        onpageindexchanging="grdDetails_PageIndexChanging" 
                                        onrowdatabound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                            
<asp:BoundField DataField="IPName" HeaderText="Name of the IP"></asp:BoundField>
<asp:BoundField DataField="WorkCode" HeaderText="Work Code"></asp:BoundField>
<asp:BoundField DataField="InvoiceAmount" HeaderText="Invoice Amount" /> 
                                            <asp:BoundField DataField="InvoiceDate" HeaderText=" Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                          <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:hyperlink runat="server" class="fa fa-edit" id="hlRedirect" navigateurl='<%# String.Format("ApproveInvoice.aspx?invoice={0}&WorkId={1}", Eval("invoiceid"), Eval("intWorkid")) %>' text='Update'>
</asp:hyperlink>
                                                   <%-- <asp:LinkButton ID="LinkButton1"  CommandArgument ='<%#Eval("invoiceid"+"")%>' class="fa fa-edit" runat="server" OnClick="LinkButton1_Clicked">Update</asp:LinkButton>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </columns>
                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <alternatingrowstyle backcolor="White" />
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

