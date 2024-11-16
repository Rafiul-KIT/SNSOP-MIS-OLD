<%@ Page Title=":: :: SNSOP : : Wages Payment" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"AutoEventWireup="true" Inherits="PDCHouseholdWages" Codebehind="PDCHouseholdWages.aspx.cs" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>
    
     <script type="text/javascript">
         //<![CDATA[
         function CheckAll(oCheckbox) {
             var GridView2 = document.getElementById("<%=grdDetails.ClientID %>");
             for (i = 1; i < GridView2.rows.length; i++) {
                 GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
             }
         }

         //]]>
 </script>

      
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

       window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

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
                                <i class="fa fa-fw fa-desktop"></i>  PDC
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Wages Payment</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Wages Payment</h3>
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
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="128px">Project Name :</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblProject" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Work Code :</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblWorkCode" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label417" runat="server" CssClass="LBLBLACK" Width="137px">Approved 
                                                        Date :</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblWorkStartDate" runat="server" CssClass="LBLBLACK" 
                                                            Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label412" runat="server" CssClass="LBLBLACK" Width="128px">From Date :</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                         <asp:TextBox ID="TxtFromDate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtStartDate2_CalendarExtender" runat="server" 
                                                            Format="dd-MM-yyyy" PopupButtonID="TxtFromDate" TargetControlID="TxtFromDate">
                                                        </cc1:CalendarExtender></td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                            ControlToValidate="TxtFromDate" ErrorMessage="Please Enter From Date" 
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                               
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label413" runat="server" CssClass="LBLBLACK" Width="128px">To Date :</asp:Label>
                                                    </td>
                                                   <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                         <asp:TextBox ID="TxtToDate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                            Format="dd-MM-yyyy" PopupButtonID="TxtToDate" TargetControlID="TxtToDate">
                                                        </cc1:CalendarExtender></td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                            ControlToValidate="TxtToDate" ErrorMessage="Please Enter To Date" 
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr id="tr_group_search" runat="server" visible="false">
                                                    <td>
                                                         <tr>
                                                     <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="100px">Group Type :</asp:Label>
                                                    </td>
                                                     
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddl_group_type" runat="server" class="form-control txtbox" 
                                                            AutoPostBack="True" Width="178px"
                                                            onselectedindexchanged="ddl_group_type_SelectedIndexChanged">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                             <asp:ListItem Value="Group">Group</asp:ListItem>
                                                             <asp:ListItem Value="CST">CST</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                         <td  runat="server"></td>
                                                              <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                </tr>

                                                <tr id="tr_group_code" runat="server" visible="false">
                                                       <td style="padding: 5px; margin: 5px" >
                                                        <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK">Group Code :</asp:Label>
                                                    </td>
                                                    
                                                    <td style="padding: 5px; margin: 5px" >
                                                        <asp:DropDownList ID="ddl_group_code" runat="server" class="form-control txtbox" 
                                                             Width="178px" AutoPostBack="True" OnSelectedIndexChanged="ddl_group_code_SelectedIndexChanged">
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
                                                   
                                                          
                                                                 <td style="padding: 5px; margin: 5px" >
                                                         <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-primary" 
                                        Height="32px"  TabIndex="10" Text="Search"  Visible="false"
                                        Width="90px" />
                                                        </td>
                                                </tr>
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
                                                        <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblState" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label428" runat="server" CssClass="LBLBLACK" Width="165px">County</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblCounty" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label429" runat="server" CssClass="LBLBLACK" Width="165px">Payam</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblPayam" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">Boma 
                                                        Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblBoma" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                              
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label430" runat="server" CssClass="LBLBLACK" Width="128px">Wage 
                                                        Rate(USD)</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtAmount" runat="server" AutoPostBack="True" 
                                                            class="form-control txtbox" Height="28px" MaxLength="4" 
                                                            onkeypress="NumberOnly()" ontextchanged="txtAmount_TextChanged" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px">3</asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                            ControlToValidate="txtAmount" ErrorMessage="Please Enter Wage Rate" 
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                              
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label431" runat="server" CssClass="LBLBLACK" Width="128px">Exchange Rate</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtExchangerate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="8" onkeypress="DecimalOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px" 
                                                            ontextchanged="txtExchangerate_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                                            ControlToValidate="txtExchangerate" ErrorMessage="Please Enter Exchange Rate" 
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                              
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="BtnSave3" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave3_Click" TabIndex="10" Text="Search" 
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnSave4" runat="server" CssClass="btn btn-danger" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Cancel" 
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:GridView ID="grdDetails" runat="server" 
                                                AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" 
                                                DataKeyNames="intBenid" ForeColor="#333333" Height="62px" 
                                                onrowdatabound="grdDetails_RowDataBound" PageSize="15" Width="100%" 
                                                ShowFooter="True">
                                                <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                                <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                                    verticalalign="Middle" />
                                                <columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex +1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="All">
                                                        <HeaderTemplate>
                                                            <%-- <asp:CheckBox ID="CheckBox2" runat="server" />--%>
                                                            <asp:CheckBox ID="ChKAll" runat="server" __designer:wfdid="w5" 
                                                                AutoPostBack="True" onclick="CheckAll(this);" />
                                                            &nbsp;
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" __designer:wfdid="w4" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                                    <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                                    <asp:BoundField DataField="NameofHousehold" HeaderText="Name" />
                                                    <asp:BoundField DataField="Sex" HeaderText="Gender" />
                                                    <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                                    <asp:BoundField DataField="Email" HeaderText="Email" Visible="false" />
                                                    <asp:BoundField DataField="FamilyIncome" HeaderText="Family Income" Visible="false" />
                                                    
                                                    <asp:BoundField DataField="cnt" HeaderText="No of Days Present" />
                                                    
                                                    <asp:BoundField HeaderText="Amount to be Paid" Visible="False" /> <%--DataField="Amt" --%>
                                                    <asp:BoundField  HeaderText="Paid Amount" Visible="False" />
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            
                                                            <asp:TextBox ID="txtAmt" onkeypress="NumberOnly()" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="6" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px; text-align: left;" valign="top">
                                           <table>
                                               <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label416" runat="server" CssClass="LBLBLACK" Width="128px">Paid Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" 
                                                            Format="dd-MM-yyyy" PopupButtonID="txtStartDate" TargetControlID="txtStartDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                     
                                                    </td>
                                                </tr></table></td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Submit" 
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger" 
                                                Height="32px" onclick="BtnSave2_Click" TabIndex="10" Text="Cancel" 
                                                ValidationGroup="group" Width="90px" Visible="False" />
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
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                            </div>
                        </div>
                    </div>
                </div>

    </div>
               
                 
         
                       
  </ContentTemplate>
  </asp:UpdatePanel>
 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

