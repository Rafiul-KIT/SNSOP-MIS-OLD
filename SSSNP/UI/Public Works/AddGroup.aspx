<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="AddGroup" Codebehind="AddGroup.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      
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

    window.open("Lookups/GroupLookUp.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

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
<div align="left" style="margin-top :-10px;">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="Home.aspx">Dashboard</a>
                            </li>
                            <li class="">
                                <i class="fa fa-fw fa-desktop"></i>  PDC
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Group Master</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Group Master</h3>
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
                                                        <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="128px">State</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                      <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px" 
                                                            onselectedindexchanged="ddlState_SelectedIndexChanged" AutoPostBack="True">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                            ControlToValidate="ddlState" ErrorMessage="Please Select State" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">County</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                       <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox" 
                                                            Height="28px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlCounties_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                            ControlToValidate="ddlCounties" ErrorMessage="Please Select County" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">Payam</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                      <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="ddlPayam" ErrorMessage="Please Select Payam" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                               
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label428" runat="server" CssClass="LBLBLACK" Width="165px">Boma</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                      <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="ddlBoma" ErrorMessage="Please Select Boma" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator></td>
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
                                                        <asp:Label ID="Label429" runat="server" CssClass="LBLBLACK" Width="165px">Group Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                       <asp:DropDownList ID="ddlGroupType" runat="server" class="form-control txtbox" AutoPostBack="true" onselectedindexchanged="ddlGroupType_SelectedIndexChanged"
                                                            Height="28px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Group</asp:ListItem>
                                                            <asp:ListItem>CST</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    
                                                        <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="ddlGroupType" ErrorMessage="Please Select Group Type" 
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="trgroupcode" runat="server">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">Group Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddl_group_code" runat="server" class="form-control txtbox" 
                                                            Height="28px" AutoPostBack="True">
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
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                       

                                                    </td>
                                                </tr>
                                                <tr  id="trMeeting" runat="server">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label408" runat="server" CssClass="LBLBLACK" Width="165px">Group Strength</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtGroupStrength" runat="server" class="form-control txtbox"
                                                        Height="28px"    Width="192px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="txtGroupStrength" 
                                                            ErrorMessage="Please Enter Group Strength" SetFocusOnError="True" 
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                                
                                            </table>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    
                                    <tr>
                                        
                                    </tr>
                                    
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Submit" 
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger" 
                                                Height="32px" onclick="BtnCancel_Click" TabIndex="10" Text="Cancel" CausesValidation="false" 
                                                ValidationGroup="group" Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <div ID="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" 
                                                    href="#">×</a> <strong>Success!</strong><asp:Label 
                                                    ID="lblmsg" runat="server"></asp:Label>
                                            </div>
                                            
                                            <div ID="Failure" runat="server" class="alert alert-danger" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>
                                                Warning!</strong>
                                                <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    
                                </table>
                                 <asp:HiddenField ID="hdGroupStrength" runat="server" Value="0" />
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile1" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                            </div>
                        </div>
                    </div>
     <div class="col-lg-11" style="margin-bottom:200px;" >
         
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

