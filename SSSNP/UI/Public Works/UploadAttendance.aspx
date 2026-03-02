<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/ImportmasterPageLogin.master" AutoEventWireup="true" Inherits="UploadAttendance" Codebehind="UploadAttendance.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">






    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;" valign="top" align="center">
                <table id="table83" border="0" cellpadding="0" cellspacing="0" class="" 
                    style="text-align: center; width: 92%;">
                    <%--<tr>
                        <td style="background-position: left top; background-image: url(resources/images/TableImages/table_BgTop.png);
                background-repeat: repeat-x; text-align: left">
                            <img alt="" height="23" src="resources/images/TableImages/table_LTop.png" width="30" /></td>
                        <td style="background-position: center top; font-weight: bold; font-size: 11px; background-image: url(resources/images/TableImages/table_BgTop.png);
                vertical-align: middle; color: #ffffff; background-repeat: repeat-x; font-family: Verdana;
                text-align: center">
                            Upload
                            Trainee&nbsp;</td>
                        <td style="background-position: right top; background-image: url(resources/images/TableImages/table_BgTop.png);
                background-repeat: repeat-x; text-align: right">
                            <img alt="" height="23" src="resources/images/TableImages/table_RTop.png" width="30" /></td>
                    </tr>--%>
                    <tr style="vertical-align: top">
                        <td bordercolor="#093461" colspan="3" style="border-right: #093461 1px solid;
                padding-right: 10px; border-top: #093461 1px solid; padding-left: 10px;
                border-left: #093461 1px solid; padding-top: 2px; border-bottom: #093461 1px solid; height: 374px;">
                            <table style="width: 100%; margin-right: 0px;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="center" 
                            style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 20px; height: 216px; vertical-align: top;" 
                            valign="top" width="100%">
                                        <br />
                                       <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="vertical-align: top; width: 50%; text-align: center">
                                                    <table style="width: 95%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="left" 
                                                    style="padding: 5px; vertical-align: top; text-align: left; " 
                                                    valign="top">
                                                                <table cellpadding="2" style="width: 100%">
                                                                    <tr>
                                                                        <td align="center" valign="top">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;</td>
                                                                                    <td>
                                                                                         <a href="frmAttendanceUpload.aspx" class="btn btn-primary">Download Template</a></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                <asp:Label ID="Label91" runat="server" CssClass="LBLBLACK" 
                                                                    Text="Upload Attendance for Benificiaries&lt;font color=red&gt;*&lt;/font&gt;" Width="166px"></asp:Label>&nbsp;</td>
                                                                                    <td>
                                                                                       <asp:FileUpload id="fupLetter" runat="server" Width="160px"  ></asp:FileUpload>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                    <td align="left" style="padding-bottom: 5px; padding-top: 5px">
                                                                                        <asp:Button ID="btnSend" runat="server" CssClass="BUTTONLONG" 
                                                                                            onclick="btnSend_Click" Text="Submit" Width="123px" />
                                                                                    </td>
                                                                                </tr>
                                                                                 <tr>
                                                                                   <td>
                                                                                    </td>
                                                                                    <td>
                                                                                   <asp:Label ID="lblresult0" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="Green" Style="position: static"></asp:Label>
                                                                                    </td>
                                                                                  </tr>

                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                    
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                <asp:Label ID="Label97" runat="server" CssClass="LBLBLACK" 
                                                                    
                                                                                Text="Note :" 
                                                                                Width="826px" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                <asp:Label ID="Label92" runat="server" CssClass="LBLBLACK" 
                                                                    
                                                                                Text="1. Download the template." 
                                                                                Width="826px" Font-Bold="True" ForeColor="Red" Font-Size="18px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <td>
                                                                    <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" 
                                                                    
                                                                                Text="2. Include your data in Beneficiary Attendance sheet." 
                                                                                Width="826px" Font-Bold="True" ForeColor="Red"  Font-Size="18px"></asp:Label>
                                                                    </td>
                                                                    <tr>
                                                                        <td>
                                                                    <asp:Label ID="Label96" runat="server" CssClass="LBLBLACK" 
                                                                    
                                                                                Text="3. Do not open excel while uploading"
                                                                                Width="826px" Font-Bold="True" ForeColor="Red"  Font-Size="18px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                      <tr>
                                                                        <td>
                                                                    <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" 
                                                                    
                                                                                Text="4. Date must be in yyyy-mm-dd format"  Width="826px" Font-Bold="True" ForeColor="Red"  Font-Size="18px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                  
                                                                    </table>
                                                            </td>
                                                        </tr>
                                                        <tr id="trstat" runat ="server" visible="false">
                                                            <td align="center" valign="top" id="trChildsssss" runat="server" 
                                                                style="padding: 10px">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="Label94" runat="server" CssClass="LBLBLACK" 
                                                                    Text="Ported Attendance" Width="166px" Font-Bold="True"></asp:Label>&nbsp;
                                                                                        <asp:Button ID="btnSend0" runat="server" CssClass="BUTTONLONG" 
                                                                                            onclick="btnSend0_Click" Text="Export" Width="123px" />
                                                                                    </td>
                                                                                    <td>
                                                                                       &nbsp; &nbsp; &nbsp;  &nbsp;<asp:Label ID="Label93" runat="server" CssClass="LBLBLACK" 
                                                                    Text="Failed Attendance" Width="166px" Font-Bold="True"></asp:Label>
                                                            &nbsp; <asp:Button ID="btnSend1" runat="server" CssClass="BUTTONLONG" 
                                                                                            onclick="btnSend1_Click" Text="Export" 
                                                                    Width="123px" />
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" style="padding-bottom: 5px; padding-top: 5px" valign="top">
                                                                                        <asp:Panel ID="Panel2" runat="server" Width="550px" ScrollBars="Horizontal">
                                                                                      
                                                                                    
                                                                    <asp:GridView ID="GridView2" runat="server" BorderWidth="1px" CellPadding="5"  AutoGenerateColumns="false"
    ForeColor="#333333" tabIndex="58" Width="100%" 
    Font-Names="verdana" Font-Size="13px">
                                                                        <RowStyle BackColor="#EFF3FB" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="S.No">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItemIndex +1 %>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Work Code" DataField="Work Code" />
                                                                            <asp:BoundField HeaderText="Enrollment No" DataField="Enrollment No" />
                                                                             <asp:BoundField HeaderText="Attendance Date" DataField="Attendance Date" />
                                                                            <asp:BoundField HeaderText="Status" DataField="Status" />
                                                                           
                                                                            
                                                                            <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                                                                        </Columns>
                                                                        <FooterStyle BackColor="#093461" Font-Bold="True" 
        ForeColor="White" />
                                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" 
        HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" 
        ForeColor="#333333" />
                                                                        <HeaderStyle BackColor="#093461" Font-Bold="True" 
        ForeColor="White" />
                                                                        <EditRowStyle BackColor="#2461BF" />
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                    </asp:GridView>
                                                                    
                                                                      </asp:Panel>
                                                                    
                                                                                    </td>
                                                                                    <td align="left" 
                                                                                        style="padding-bottom: 5px; padding-top: 5px; padding-left: 20px;" valign="top">
                                                                                    
                                                                                    <asp:Panel ID="Panel3" runat="server"  Width="550px" ScrollBars="Horizontal">
                                                                                      
                                                                                    
                                                                    <asp:GridView ID="GridView1" runat="server" BorderWidth="1px" CellPadding="5"  AutoGenerateColumns="false"
    ForeColor="#333333" tabIndex="58" Width="100%" 
    Font-Names="verdana" Font-Size="13px">
                                                                        <RowStyle BackColor="#EFF3FB" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="S.No">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItemIndex +1 %>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" Width="70px"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                             <asp:BoundField HeaderText="Work Code" DataField="Work Code" />
                                                                            <asp:BoundField HeaderText="Enrollment No" DataField="Enrollment No" />
                                                                             <asp:BoundField HeaderText="Attendance Date" DataField="Attendance Date" />
                                                                            <asp:BoundField HeaderText="Status" DataField="Status" />
                                                                           
                                                                            
                                                                            <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                                                                        </Columns>
                                                                        <FooterStyle BackColor="#093461" Font-Bold="True" 
        ForeColor="White" />
                                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" 
        HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" 
        ForeColor="#333333" />
                                                                        <HeaderStyle BackColor="#093461" Font-Bold="True" 
        ForeColor="White" />
                                                                        <EditRowStyle BackColor="#2461BF" />
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                    </asp:GridView>
                                                                     </asp:Panel>
                                                                    
                                                                    
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                            </td>
                                                        </tr>
                                                       
                                                        
                                                        
                                                        <tr>
                                                            <td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                    vertical-align: top; padding-top: 5px; text-align: left" valign="top" width="100%">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="text-align: left; padding-right: 5px; padding-left: 5px; padding-bottom: 5px; vertical-align: top; padding-top: 5px;" 
                                                                width="19%">
                                                                &nbsp;</td>
                                                                        <td style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;" 
                                                                width="3%">
                                                                &nbsp;</td>
                                                                        <td style="font-size: 11pt; line-height: 115%; font-family: 'Calibri','sans-serif';">
                                                                            <asp:HiddenField ID="hdfID" runat="server" />
                                                                            <asp:HiddenField ID="hdfFlagID" runat="server" />
                                                                            <br />
                                                                            <br />
                                                                            <asp:HiddenField ID="hfdClusterid" runat="server" />
                                                                            <asp:HiddenField ID="hdfupload" runat="server" />
                                                                            <asp:HiddenField ID="hdfcount" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                &nbsp;<asp:Label ID="lblError" runat="server" CssClass="LBLBLACK" ForeColor="Red" 
                                                        Width="300px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" 
                                                    style="padding: 5px; vertical-align: top; margin-left: 40px;" 
                                                    valign="top">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" 
                                                    style="padding: 5px; vertical-align: top; " 
                                                    valign="top">
                                                    &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" 
                                                    style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                    vertical-align: middle; padding-top: 5px; height: 35px; text-align: center" 
                                                    valign="middle">
                                                                &nbsp;&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    <asp:Label ID="lblresult" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="Green" Style="position: static"></asp:Label>
                                                    <asp:ValidationSummary ID="vg" runat="server" ForeColor="Green" ShowMessageBox="True"
                                            ShowSummary="False" Style="position: static" ValidationGroup="group" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
</asp:Content>

