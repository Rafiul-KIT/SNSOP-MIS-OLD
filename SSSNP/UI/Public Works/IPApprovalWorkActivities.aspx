<%@ Page Title=":: :: SNSOP : : Approved Works " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="IPApprovalWorkActivities" Codebehind="IPApprovalWorkActivities.aspx.cs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   

    <script type="text/javascript" language="javascript">

function OpenPopup() 
 
   {

    window.open("Lookups/CurriculamLookUp.aspx","List","scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

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
                                <i class="fa fa-fw fa-edit"></i> IP
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Approved Works</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Approved Works</h3>
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
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            &nbsp;</td>
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
                                                        <asp:Label ID="Label298" runat="server" CssClass="LBLBLACK" Width="128px">IP</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label381" runat="server" CssClass="LBLBLACK" Width="128px">Test IP</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label380" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label382" runat="server" CssClass="LBLBLACK" Width="128px">Safty Nuts</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label384" runat="server" CssClass="LBLBLACK" Width="124px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label385" runat="server" CssClass="LBLBLACK" Width="128px">Central Equatoria</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
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
                                                        <asp:Label ID="Label383" runat="server" CssClass="LBLBLACK" Width="136px">Target Beneficiaries</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label386" runat="server" CssClass="LBLBLACK" Width="128px">20000</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label387" runat="server" CssClass="LBLBLACK" Width="144px">Project Cost</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label388" runat="server" CssClass="LBLBLACK" Width="128px">2000000</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label350" runat="server" CssClass="LBLBLACK" Width="144px">Meeting Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label410" runat="server" CssClass="LBLBLACK" Width="128px">12-04-2015</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label396" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="233px">Members Involved</asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left" colspan="3">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td align="left" style="padding: 5px; margin: 5px; width: 218px;">
                                                        <asp:Label ID="Label393" runat="server" CssClass="LBLBLACK" Width="128px">BDC 
                                                        Members</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 219px;">
                                                        <asp:Label ID="Label392" runat="server" CssClass="LBLBLACK" Width="128px">PDC 
                                                        Members</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label395" runat="server" CssClass="LBLBLACK" Width="128px">County 
                                                        Members</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="128px">TST 
                                                        Members</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="padding: 5px; margin: 5px; width: 218px;">
                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" CellPadding="3" 
                                                            CellSpacing="5" RepeatColumns="2" RepeatDirection="Horizontal">
                                                            <asp:ListItem>Ramana</asp:ListItem>
                                                            <asp:ListItem>Srinivas</asp:ListItem>
                                                            <asp:ListItem>Raghava</asp:ListItem>
                                                            <asp:ListItem>Sunitha</asp:ListItem>
                                                            <asp:ListItem>Ramya</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 219px;">
                                                        <asp:CheckBoxList ID="CheckBoxList3" runat="server" CellPadding="3" 
                                                            CellSpacing="5" RepeatColumns="2" RepeatDirection="Horizontal">
                                                            <asp:ListItem>Ramana</asp:ListItem>
                                                            <asp:ListItem>Srinivas</asp:ListItem>
                                                            <asp:ListItem>Raghava</asp:ListItem>
                                                            <asp:ListItem>Sunitha</asp:ListItem>
                                                            <asp:ListItem>Ramya</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:CheckBoxList ID="CheckBoxList4" runat="server" CellPadding="3" 
                                                            CellSpacing="5" RepeatColumns="2" RepeatDirection="Horizontal">
                                                            <asp:ListItem>Ramana</asp:ListItem>
                                                            <asp:ListItem>Srinivas</asp:ListItem>
                                                            <asp:ListItem>Raghava</asp:ListItem>
                                                            <asp:ListItem>Sunitha</asp:ListItem>
                                                            <asp:ListItem>Ramya</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:CheckBoxList ID="CheckBoxList5" runat="server" CellPadding="3" 
                                                            CellSpacing="5" RepeatColumns="2" RepeatDirection="Horizontal">
                                                            <asp:ListItem>Ramana</asp:ListItem>
                                                            <asp:ListItem>Srinivas</asp:ListItem>
                                                            <asp:ListItem>Raghava</asp:ListItem>
                                                            <asp:ListItem>Sunitha</asp:ListItem>
                                                            <asp:ListItem>Ramya</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label397" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                Width="233px">Approval Work Activities</asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;</td>
                                        <td valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top" colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="800px">
                                                <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" 
                                                    AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" ForeColor="#333333" 
                                                    GridLines="None" Height="62px" onrowdatabound="grdDetails_RowDataBound" 
                                                    PageSize="15" Width="100%">
                                                    <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                                    <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                                        verticalalign="Middle" />
                                                    <columns>
                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex +1 %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="35px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="state" HeaderText="State">
                                                            <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="County" HeaderText="County">
                                                            <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Payam" HeaderText="Payam">
                                                            <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Boma" HeaderText="Boma">
                                                            <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="StartDate" DataFormatString="{0:dd-MM-yyyy}" 
                                                            HeaderText="Start Date">
                                                            <ItemStyle Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EndDate" DataFormatString="{0:dd-MM-yyyy}" 
                                                            HeaderText="End Date">
                                                            <ItemStyle Width="80px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Involved" HeaderText="No of peoples involved" />
                                                        <asp:TemplateField HeaderText="Details">
                                                            <ItemTemplate>
                                                                <asp:GridView ID="grdDetails123" runat="server" AllowPaging="True" 
                                                                    AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" 
                                                                    DataKeyNames="intMobilization" ForeColor="#333333" GridLines="Both" 
                                                                    Height="62px" PageSize="20" Width="100%">
                                                                    <rowstyle cssclass="GRDITEM" horizontalalign="Left" verticalalign="Middle" />
                                                                    <columns>
                                                                        <asp:BoundField DataField="AreaOfWork" HeaderText="Type of Work">
                                                                            <ItemStyle Width="90px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="WorkActitvity" HeaderText="Work Actitvity">
                                                                            <ItemStyle Width="80px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="workCode" HeaderText="Work Code">
                                                                            <ItemStyle Width="80px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="NoofHousedhold" HeaderText="Estimated Households">
                                                                            <ItemStyle Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="NoofDays" HeaderText="Estimated No of Days">
                                                                            <ItemStyle Width="60px" />
                                                                        </asp:BoundField>
                                                                        
                                                                         <asp:BoundField DataField="EquipmentRequired" HeaderText="Estimated Equipment Required">
                                                                            <ItemStyle Width="60px" />
                                                                        </asp:BoundField>
                                                                        
                                                                         <asp:BoundField DataField="MaterialRequired" HeaderText="Estimated Material Required">
                                                                            <ItemStyle Width="60px" />
                                                                        </asp:BoundField>
                                                                        
                                                                         <asp:BoundField DataField="SkillLabour" HeaderText="Estimated Skill Labour Required">
                                                                            <ItemStyle Width="60px" />
                                                                        </asp:BoundField>
                                                                        
                                                                         <asp:BoundField DataField="Cost" HeaderText="Estimated Cost">
                                                                            <ItemStyle Width="60px" />
                                                                        </asp:BoundField>
                                                                        
                                                                    </columns>
                                                                    <footerstyle backcolor="#83BE00" font-bold="True" forecolor="White" />
                                                                    <pagerstyle backcolor="#B9D684" forecolor="White" horizontalalign="Center" />
                                                                    <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                                                </asp:GridView>
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
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Save" 
                                                ValidationGroup="group" Width="90px" />
                                            <asp:Button ID="BtnClear" runat="server" CausesValidation="False" 
                                                CssClass="btn btn-danger" Height="32px" onclick="BtnClear_Click" TabIndex="10" 
                                                Text="ClearAll" ToolTip="To Clear  the Screen" Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

    </div>
               
                 
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          <br />
          
                       
  </ContentTemplate>
  </asp:UpdatePanel>
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

