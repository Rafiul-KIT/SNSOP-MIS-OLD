<%@ Page Title=":: :: SNSOP : : Work Progress " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"  AutoEventWireup="true" Inherits="PDCWorkProgress" Codebehind="PDCWorkProgress.aspx.cs" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

     <%--   function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdDetails.ClientID %>");
              for (i = 1; i < GridView2.rows.length; i++) {
                  GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
              }
        }--%>

                    
        $(document).ready(function() {

            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);

            $('#txtStartDate').datetimepicker();
        });
    </script>

     <script type="text/javascript">
         function CheckAll(id) {
             var masterTable = $find("<%= RadGrid2.ClientID %>").get_masterTableView();
             var row = masterTable.get_dataItems();
             if (id.checked == true) {
                 for (var i = 0; i < row.length; i++) {
                     masterTable.get_dataItems()[i].findElement("chkSelected").checked = true; // for checking the checkboxes
                 }
             }
             else {
                 for (var i = 0; i < row.length; i++) {
                     masterTable.get_dataItems()[i].findElement("chkSelected").checked = false; // for unchecking the checkboxes
                 }
             }
         }
         function unCheckHeader(id) {
             var masterTable = $find("<%= RadGrid2.ClientID %>").get_masterTableView();
             var row = masterTable.get_dataItems();
             if (id.checked == false) {
                 var hidden = document.getElementById("HiddenField1");
                 var checkBox = document.getElementById(hidden.value);
                 checkBox.checked = false;
             }
         }
    </script>


    <script type="text/javascript" language="javascript">

        function OpenPopup() {
            //window.location.reload();
            window.open("Lookups/WorkProgressLookupNew.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

            return false;
        }

        function ApplyChanges(val, val1) {

            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

            document.forms[0].submit();
        }
    </script>

    <%-- <asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>--%>
    <div align="left">
        <ol class="breadcrumb">
            You are here &nbsp;!&nbsp; &nbsp; &nbsp;
            <li><i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a> </li>
            <li class=""><i class="fa fa-fw fa-desktop"></i>PDC </li>
            <li class="active"><i class="fa fa-edit"></i><a href="#">Work Progress</a> </li>
        </ol>
    </div>
    <div align="center">
        <div class="row" align="center">
            <div class="col-lg-11">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Work Progress</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                    <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="165px">Search</asp:Label>
                                    <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px"
                                         Text="Look Up" CausesValidation="False" Font-Size="12px"
                                        Style="position: static" ToolTip="Project Lookup" />
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
                                                &nbsp;
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:Label ID="lblProject" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="165px"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Work Code</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:Label ID="lblWorkCode" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="165px"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:Label ID="lblState" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label428" runat="server" CssClass="LBLBLACK" Width="165px">County</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:Label ID="lblCounty" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
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
                                                <asp:Label ID="Label429" runat="server" CssClass="LBLBLACK" Width="165px">Payam</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="lblPayam" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">Boma</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="lblBoma" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr id="trMeeting" runat="server">
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label408" runat="server" CssClass="LBLBLACK" Width="137px">Approved Date</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="lblWorkStartDate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                &nbsp;
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                &nbsp;
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                &nbsp;
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="padding: 5px; margin: 5px" valign="top">

                                    <telerik:radgrid id="RadGrid1" pagesize="50" autogeneratecolumns="false" runat="server" allowpaging="true" Visible="false"
                                        allowmultirowselection="true" allowsorting="true" gridlines="Both"
                                        mastertableview-tablelayout="Fixed"
                                        OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="RadGrid1_ItemDataBound">
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                     
                                        
                                <MasterTableView TableLayout="Fixed" 
                                        DataKeyNames="WorkName" 
                                        ClientDataKeyNames="WorkName" >                             
                                        <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>    
                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                               			        
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No" >
                                       <ItemTemplate>
                                            <asp:Label ID="rowNumberLabel" runat="server" />
                                        </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                    </telerik:GridTemplateColumn>

                                        <%--<telerik:GridBoundColumn DataField="intBenTempid" Display="false" HeaderText="ID" HeaderStyle-Width="8%" > </telerik:GridBoundColumn>--%>
                                                                                                                  
                                        <telerik:GridBoundColumn DataField="WorkName" HeaderText="Work Name" HeaderStyle-Width="15%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AreaofWork" HeaderText="Area of Work" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WorkActivity" HeaderText="Work Activity" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EstManPower" HeaderText="Estimated Manpower" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EstDays" HeaderText="Estimated Days" HeaderStyle-Width="10%" Visible="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EstEquipmnts" HeaderText="Estimated Equipment Required" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EstMaterial" HeaderText="Estimated Material Required" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EstSkilLabour" HeaderText="Estimated Skill Labour Required" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EstWorkStartDate" DataFormatString="{0:MM-dd-yyyy}" HeaderText="Estimated Work Start Date" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EstWorkEndDate" DataFormatString="{0:MM-dd-yyyy}" HeaderText="Estimated Work End Date" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EstCost" HeaderText="Estimated Cost" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                    

                                     
                                 </Columns>
                            </MasterTableView>

                        </telerik:radgrid>
                                </td>
                            </tr>
                            <tr>
                                

                                
                                
                                <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                   <%-- <asp:GridView ID="gvpractical0" runat="server" AutoGenerateColumns="False" BorderColor="#003399"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="4" CssClass="GRD" DataKeyNames="intWorkid,intWorkActivityId"
                                        Font-Names="Verdana" Font-Size="12px" ForeColor="#333333" GridLines="None" Width="100%">
                                        <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#FFFFFF" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WorkName" HeaderText="Work Name" />
                                            <asp:BoundField DataField="AreaofWork" HeaderText="Area of Work" />
                                            <asp:BoundField DataField="WorkActivity" HeaderText="Work Activity" />
                                            <asp:BoundField DataField="EstManPower" HeaderText="Estimated Manpower" />
                                            <asp:BoundField DataField="EstDays" HeaderText="Estimated Days" Visible="False" />
                                            <asp:BoundField DataField="EstEquipmnts" HeaderText="Estimated Equipment Required" />
                                            <asp:BoundField DataField="EstMaterial" HeaderText="Estimated Material Required" />
                                            <asp:BoundField DataField="EstSkilLabour" HeaderText="Estimated Skill Labour Required" />
                                            <asp:BoundField DataField="EstWorkStartDate" DataFormatString="{0:MM-dd-yyyy}" HeaderText="Estimated Work Start Date" />
                                            <asp:BoundField DataField="EstWorkEndDate" DataFormatString="{0:MM-dd-yyyy}" HeaderText="Estimated Work End Date" />
                                            <asp:BoundField DataField="EstCost" HeaderText="Estimated Cost" />
                                        </Columns>
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#013161" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 83%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label420" runat="server" CssClass="LBLBLACK" Width="165px">Work Start Date</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:TextBox ID="txtStartDate" runat="server" class="form-control txtbox" Height="28px"
                                                    MaxLength="10" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" PopupButtonID="txtStartDate"
                                                    TargetControlID="txtStartDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtStartDate"
                                                    ErrorMessage="Please Enter Start Date" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label426" runat="server" CssClass="LBLBLACK" Width="165px">work supervision / Incharge</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:TextBox ID="txtIncharge" runat="server" class="form-control txtbox" Height="40px"
                                                    MaxLength="100" TabIndex="1" TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtIncharge"
                                                    ErrorMessage="Please Enter Work Supervison /Incharge" SetFocusOnError="True"
                                                    ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="128px">Upload Attachments</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label435" runat="server" CssClass="LBLBLACK" Width="128px">Upload Document Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtUploadDocumentName" runat="server" class="form-control txtbox"
                                                    Height="28px" TabIndex="5" ValidationGroup="group" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUploadDocumentName"
                                                    ErrorMessage="Please Enter Document Name" SetFocusOnError="True" ValidationGroup="group1"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr id="tr_group_search" runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                         <asp:Label ID="Label3" runat="server" Visible="false" CssClass="LBLBLACK" Width="100px">Group Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                     
                                                        <asp:CheckBoxList ID="ddl_group_type" Visible="false" 
                                                            runat="server" 
                                                            onselectedindexchanged="ddl_group_type_SelectedIndexChanged" 
                                                            AutoPostBack="True" RepeatDirection="Horizontal">
                                                      <asp:ListItem Selected="True" Value="">Both</asp:ListItem>
                                                            <asp:ListItem Value="Group">Group</asp:ListItem>
                                                            <asp:ListItem Value="CST">CST</asp:ListItem>
                                                      
                                                      
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td id="Td1" runat="server">
                                                    </td>
                                                </tr>
                                               
                                                <tr id="tr_group_code" runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                         <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK">Group Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                       
                                                        
                                                        
                                                        
                                                        
                                                        <asp:Panel runat="server" ID="pnl" Height="160px" ScrollBars="Vertical">
                                                        
                                                        
                                                        <asp:CheckBoxList ID="ddl_group_code"   
                                                            runat="server" 
                                                            OnSelectedIndexChanged="ddl_group_code_SelectedIndexChanged" 
                                                            AutoPostBack="True" RepeatColumns="4">
                                                    
                                                         <asp:ListItem Selected="True" Value="">All</asp:ListItem>
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
                                                      
                                                      
                                                        </asp:CheckBoxList>
                                                        
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-primary" 
                                                            Height="32px" TabIndex="10" Text="Search" Visible="false" Width="90px" />
                                                    </td>
                                                </tr>
                                               
                                    </table>
                                </td>
                                <td style="width: 27px">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label421" runat="server" CssClass="LBLBLACK" Width="165px">Status</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control txtbox" Height="33px">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem>InProgress</asp:ListItem>
                                                    <asp:ListItem>Completed</asp:ListItem>
                                                </asp:DropDownList>
                                           
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlStatus"
                                                    ErrorMessage="Please Select Status" InitialValue="--Select--" SetFocusOnError="True"
                                                    ValidationGroup="group"><span style="color: red;">&ensp;*</span></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label422" runat="server" CssClass="LBLBLACK" Width="165px">Remarks</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:TextBox ID="txtRemarks" runat="server" class="form-control txtbox" Height="60px"
                                                    MaxLength="100" TabIndex="1" TextMode="MultiLine" ValidationGroup="group" Width="200px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label376" runat="server" CssClass="LBLBLACK" Width="165px">Upload Document</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:FileUpload ID="FileWorkPhotoUpload" runat="server" class="form-control txtbox"
                                                    Height="32px" Width="180px" />
                                                <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                &nbsp;
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                &nbsp;
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-xs btn-warning" Height="28px"
                                                    TabIndex="10" Text="Add" ValidationGroup="group1" Width="72px" OnClick="btnAddNew_Click" />
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                &nbsp;
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
                                <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                      <telerik:radgrid id="RadAttachment" pagesize="50" autogeneratecolumns="false" runat="server" allowpaging="true" Visible="false"
                                        allowmultirowselection="true" allowsorting="true" gridlines="Both"
                                        mastertableview-tablelayout="Fixed" OnNeedDataSource="RadAttachment_NeedDataSource" OnItemDataBound="RadAttachment_ItemDataBound"
                                         OnItemCommand="RadAttachment_ItemCommand" >
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                         
                                    
                                <MasterTableView TableLayout="Fixed" 
                                        DataKeyNames="" 
                                        ClientDataKeyNames="" >                             
                                        <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>    
                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                               			        
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No" >
                                       <ItemTemplate>
                                            <asp:Label ID="rowNumberLabel" runat="server" />
                                        </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                    </telerik:GridTemplateColumn>
                                                                                                                                                      
                                        <telerik:GridButtonColumn CommandName="cmd_delete" Text="Delete" HeaderText="Delete" HeaderStyle-Width="25%"></telerik:GridButtonColumn>
                                        
                                            <telerik:GridTemplateColumn UniqueName="DownloadColumn" HeaderText="Document Name" >
                                              <ItemTemplate>
                                                   <asp:LinkButton ID="lnkDownload"  runat="server" Text ='<%# Eval("DocumentName") %>' CommandArgument='<%# Eval("WorkPhoto") %>'  CommandName="cmd_download" />
                                                                                        
                                             </ItemTemplate>
                                               <HeaderStyle Width="80%" />
                                             </telerik:GridTemplateColumn>

                                         
                                     
                                 </Columns>
                            </MasterTableView>

                        </telerik:radgrid>
                                </td>
                            </tr>
                            <tr>
                         <%--       <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                    <asp:GridView ID="grdAttachments" runat="server" AutoGenerateColumns="False" BorderColor="#003399"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="4" CssClass="table table-bordered table-responsive"
                                        DataKeyNames="intWorkid,DocumentName,WorkPhoto,Status,IntWorkDetailsTypeId,Created_by,Created_dt"
                                        Font-Names="Verdana" Font-Size="12px" ForeColor="#333333" GridLines="None" OnRowDeleting="grdAttachments_RowDeleting"
                                        Width="100%" OnSelectedIndexChanged="grdAttachments_SelectedIndexChanged" OnRowCommand="grdAttachments_RowCommand">
                                        <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#FFFFFF" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                    <asp:HiddenField ID="hdfAttachmentsName" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:CommandField HeaderText="Select" ShowSelectButton="True" Visible="false" />
                                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="Document Name" ItemStyle-CssClass="Shorter" SortExpression="description">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownloadMaterialRequire" Text='<%# Eval("DocumentName") %>'
                                                        CommandArgument='<%# Eval("WorkPhoto") %>' runat="server" CommandName="download"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#013161" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>--%>
                            </tr>

                            <tr>
                                 <td colspan="3" style="padding: 5px; margin: 5px" valign="top">

                                    <telerik:radgrid id="RadGrid2"  autogeneratecolumns="false" runat="server"  Visible="false"
                                        allowmultirowselection="true" allowsorting="true" gridlines="Both"
                                        mastertableview-tablelayout="Fixed"
                                        OnNeedDataSource="RadGrid2_NeedDataSource" OnItemDataBound="RadGrid2_ItemDataBound"> 
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                     
                                <MasterTableView TableLayout="Fixed" 
                                        DataKeyNames="intBenid" 
                                        ClientDataKeyNames="intBenid" >                             
                                        <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>    
                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                               			        
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No" >
                                       <ItemTemplate>
                                            <asp:Label ID="rowNumberLabel" runat="server" />
                                        </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                    </telerik:GridTemplateColumn>
                                                               
                                    <telerik:GridTemplateColumn UniqueName="checkColumn">
                                         <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAll(this)" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelected" runat="server"  onclick="unCheckHeader(this)" />
                                            </ItemTemplate>
                                         <HeaderStyle Width="40px" />
                                        </telerik:GridTemplateColumn>  

                                    <telerik:GridBoundColumn DataField="intBenid" HeaderText="intBenid" HeaderStyle-Width="15%" Visible="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderText="EnrollmentNo" HeaderStyle-Width="15%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma Name" HeaderStyle-Width="15%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NameofHousehold" HeaderText="Name" HeaderStyle-Width="30%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="gender" HeaderText="Gender" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Age" HeaderText="Age" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ContactNo" HeaderText="Contact No" HeaderStyle-Width="10%" ></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" HeaderStyle-Width="15%" Visible="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SelectionCriteria" HeaderText="Component" HeaderStyle-Width="15%" Visible="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FamilyIncome" HeaderText="Average Household Income" HeaderStyle-Width="20%"></telerik:GridBoundColumn>
                                       
                                     
                                 </Columns>
                            </MasterTableView>

                        </telerik:radgrid>
                                </td>
                            </tr>

                                    
                           <tr>
                                <td colspan="3" style="padding: 5px; margin: 5px; width: 100%;" valign="top">
                            
<%--                                    <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        CssClass="GRD" ForeColor="#333333" GridLines="None" Height="62px" PageSize="15"
                                        Width="100%" DataKeyNames="intBenid" OnRowDataBound="grdDetails_RowDataBound"
                                        OnPageIndexChanging="grdDetails_PageIndexChanging" OnSelectedIndexChanging="grdDetails_SelectedIndexChanging">
                                        <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex +1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="All">
                                                <HeaderTemplate>
                                                    
                                                    <asp:CheckBox ID="ChKAll" runat="server" AutoPostBack="True" __designer:wfdid="w5"
                                                        onclick="CheckAll(this);"></asp:CheckBox>&nbsp;
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" __designer:wfdid="w4"></asp:CheckBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                            <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                            <asp:BoundField DataField="NameofHousehold" HeaderText="Name" />
                                            <asp:BoundField DataField="gender" HeaderText="Gender" />
                                            <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" Visible="false" />
                                            <asp:BoundField DataField="FamilyIncome" HeaderText="Average Household Income" />
                                        </Columns>
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#B9D684" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>--%>


                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                        OnClick="BtnSave_Click" TabIndex="10" Text="Submit" ValidationGroup="group" Width="90px" />
                                    &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger" Height="32px"
                                        OnClick="BtnSave_Click" TabIndex="10" Text="Cancel" ValidationGroup="group" Width="90px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <div id="success" runat="server" class="alert alert-success" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert" href="AddQualification.aspx">
                                            ×</a> <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
                                    </div>
                                    <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                                        <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdfID" runat="server" />
                        <asp:HiddenField ID="hdfUploadFile1" runat="server" />
                        <asp:HiddenField ID="hdfFlagID" runat="server" />
                        <asp:HiddenField ID="hdfbomaid" runat="server" />
                        <br />
                        <asp:HiddenField ID="hdfworkid" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="group" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="group1" />
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
