<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="IPPrintCardID.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.IPPrintCardID" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <script  type="text/javascript">

          function modifyCommand(imageEditor, args) {
              if (args.get_commandName() == "Crop") {
                  waitForCommand(imageEditor, args.get_commandName(), function (widget) {
                      var width = 140;
                      var height = 190;
                      var ratio = width / height;

                      //stop the aspect ratio constraint
                      //widget._constraintBtn.set_checked(false); 
                      //widget._setCropBoxRatio(null);
                      //widget._sizeRatio = null;

                      widget._setCropBoxRatio(ratio);
                      widget._sizeRatio = ratio;
                      widget.set_width(width);
                      widget.set_height(height);
                      widget._constraintBtn.set_enabled(false);
                      widget._updateCropBoxFromControls();
                  });
              }
          }

          function openRadWindow(typeSource) {

              if (typeSource == 0) {
                  var value = document.getElementById("hdPhotoMain").value;
              } else if (typeSource == 1) {
                  var value = document.getElementById("hdPhotoA1").value;
              } else if (typeSource == 2) {
                  var value = document.getElementById("hdPhotoA2").value;
              }
              // document.getElementById("btnGenerateFilename").click();
              
                          
              var oWnd = radopen("../../PhotoEditorTool.aspx?type=1&filename=" + value, "RadWindow1"); //Pass parameter using URL   
              oWnd.center();
              return false;
          }

    </script>

    <script src="../Resource/js/jquery.js" type="text/javascript"></script>
    <script src="../Resource/js/bootstrap.min.js"></script>   
    <script src="../Resource/js/plugins/morris/raphael.min.js"></script>
    <script src="../Resource/js/plugins/morris/morris.min.js"></script>
    <script src="../Resource/js/plugins/morris/morris-data.js"></script>
    
    
    <script src="../../Resource/js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

         function OpenPopup() {

             window.open("Lookups/JobCardLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

             return false;
         }

         function ApplyChanges(val, val1) {

             document.getElementById("<%=hdfID.ClientID%>").value = val;
             document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

             document.forms[0].submit();
        }

        $(document).ready(function () {

            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);

            $('#txtStartDate').datetimepicker();
        });

       

    </script>

 
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>


    <asp:HiddenField ID ="hdPhotoMain" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID ="hdPhotoA1" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID ="hdPhotoA2" ClientIDMode="Static" runat="server" />
<div align="left">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="Home.aspx">Dashboard</a>
                            </li>
                            <li class="">
                                <i class="fa fa-fw fa-desktop"></i>  Implementing Partner
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Print ID Card</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Beneficiary Print ID Card</h3>
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
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Job Card No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblJobcard" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="160px">Name of household head</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblHHname" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label359" runat="server" CssClass="LBLBLACK" Width="137px">Name of spouse</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblSpouse" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label360" runat="server" CssClass="LBLBLACK" Width="154px">Date of Registration </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblRegDate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label371" runat="server" CssClass="LBLBLACK" Width="165px">Sex</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblGender" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label372" runat="server" CssClass="LBLBLACK" Width="165px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; ">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblAge" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label380" runat="server" CssClass="LBLBLACK" Width="165px">Average Household Income</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblIncome" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                     </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblState" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label365" runat="server" CssClass="LBLBLACK" Width="165px">County Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblCounti" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label377" runat="server" CssClass="LBLBLACK" Width="137px">Payam Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblPayam" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
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
                                                        <asp:Label ID="Label379" runat="server" CssClass="LBLBLACK" Width="137px">Boma Name</asp:Label>
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
                                                        <asp:Label ID="Label352" runat="server" CssClass="LBLBLACK" Width="137px">Beneficiary Address</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblAddress" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label298" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblContactNo" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label376" runat="server" CssClass="LBLBLACK" Width="156px">Beneficiary Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <%--<asp:Image ID="Image1" runat="server" Height="154px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />--%>
                                                        <img id="Image1" runat="server" src="~/Resource/Images/not-available.jpg" style="width: 210px; height: 236px" alt="" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="156px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                    <td style="padding-left: 10px; padding-bottom: 20px; align-content: center;">
                                                        
                                                        <asp:DropDownList runat="server" ID="ddlBrightness" Width="60px" Visible="false"  >
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>1.1</asp:ListItem>
                                                            <asp:ListItem>1.2</asp:ListItem>
                                                            <asp:ListItem>1.3</asp:ListItem>
                                                            <asp:ListItem>1.4</asp:ListItem>
                                                            <asp:ListItem>1.5</asp:ListItem>
                                                            <asp:ListItem>1.6</asp:ListItem>
                                                            <asp:ListItem>1.7</asp:ListItem>
                                                            <asp:ListItem>1.8</asp:ListItem>
                                                            <asp:ListItem>1.9</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>2.1</asp:ListItem>
                                                            <asp:ListItem>2.2</asp:ListItem>
                                                            <asp:ListItem>2.3</asp:ListItem>
                                                            <asp:ListItem>2.4</asp:ListItem>
                                                            <asp:ListItem>2.5</asp:ListItem>
                                                            <asp:ListItem>2.6</asp:ListItem>
                                                            <asp:ListItem>2.7</asp:ListItem>
                                                            <asp:ListItem>2.8</asp:ListItem>
                                                            <asp:ListItem>2.9</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>3.1</asp:ListItem>
                                                            <asp:ListItem>3.2</asp:ListItem>
                                                            <asp:ListItem>3.3</asp:ListItem>
                                                            <asp:ListItem>3.4</asp:ListItem>
                                                            <asp:ListItem>3.5</asp:ListItem>
                                                            <asp:ListItem>3.6</asp:ListItem>
                                                            <asp:ListItem>3.7</asp:ListItem>
                                                            <asp:ListItem>3.8</asp:ListItem>
                                                            <asp:ListItem>3.9</asp:ListItem>
                                                            <asp:ListItem>4.0</asp:ListItem>

                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp; &nbsp;<asp:Button runat="server" ID="btnSaveImage" Text="Save" OnClick="btnSaveImage_Click" Visible="false" />
                                                        
                                                        <asp:Button runat="server" Visible="false"  CssClass="btn btn-success" ID="btnChangeBrightness" Text="Change" OnClick="btnChangeBrightness_Click" />
                                                        
                                                        <asp:TextBox runat="server" Visible="false"  ID="txtFilename" Text="" Width ="180px"></asp:TextBox>
                                                        
                                                        <telerik:RadSlider ID="RadSlider1" runat="server" AutoPostBack="true" MinimumValue="10" MaximumValue="40" OnValueChanged="RadSlider1_ValueChanged"  >
                                                        </telerik:RadSlider>

                                                        <div style="width: 180px; text-align: center; display:block; margin-left:5px ">
                                                            <asp:Button runat="server" Enabled="false" Width="160px" CssClass="btn btn-success" ID="btnEditPhotoBeneficiary" Text="Edit Photo for ID Card" OnClientClick="return openRadWindow(0);" Visible="true" />
                                                        </div>
                                                          

                                                       

                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="156px">Alternate 1 Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox runat="server" text="" ID="txtAlternate1" Visible="False" ></asp:TextBox>
                                                        </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <%--<asp:Image ID="Image1" runat="server" Height="154px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />--%>
                                                        <img id="imgAlternate1" runat="server" src="~/Resource/Images/not-available.jpg" style="width: 210px; height: 236px" alt="" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>

                                                   <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="156px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">   </td>
                                                    <td style="padding-left: 15px; padding-bottom: 20px; align-content: center;">
                                                        
                                                        <asp:DropDownList runat="server" ID="ddlBrightnessA1" Width="60px" Visible="false" >
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>1.1</asp:ListItem>
                                                            <asp:ListItem>1.2</asp:ListItem>
                                                            <asp:ListItem>1.3</asp:ListItem>
                                                            <asp:ListItem>1.4</asp:ListItem>
                                                            <asp:ListItem>1.5</asp:ListItem>
                                                            <asp:ListItem>1.6</asp:ListItem>
                                                            <asp:ListItem>1.7</asp:ListItem>
                                                            <asp:ListItem>1.8</asp:ListItem>
                                                            <asp:ListItem>1.9</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>2.1</asp:ListItem>
                                                            <asp:ListItem>2.2</asp:ListItem>
                                                            <asp:ListItem>2.3</asp:ListItem>
                                                            <asp:ListItem>2.4</asp:ListItem>
                                                            <asp:ListItem>2.5</asp:ListItem>
                                                            <asp:ListItem>2.6</asp:ListItem>
                                                            <asp:ListItem>2.7</asp:ListItem>
                                                            <asp:ListItem>2.8</asp:ListItem>
                                                            <asp:ListItem>2.9</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>3.1</asp:ListItem>
                                                            <asp:ListItem>3.2</asp:ListItem>
                                                            <asp:ListItem>3.3</asp:ListItem>
                                                            <asp:ListItem>3.4</asp:ListItem>
                                                            <asp:ListItem>3.5</asp:ListItem>
                                                            <asp:ListItem>3.6</asp:ListItem>
                                                            <asp:ListItem>3.7</asp:ListItem>
                                                            <asp:ListItem>3.8</asp:ListItem>
                                                            <asp:ListItem>3.9</asp:ListItem>
                                                            <asp:ListItem>4.0</asp:ListItem>

                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp; &nbsp;
                                                        <asp:Button runat="server" CssClass="btn btn-success" ID="btnChangeA1" Text="Change" OnClick="btnChangeA1_Click" Visible="false" />
                                                        
                                                        <asp:TextBox runat="server" ID="txtPhotoCardA1" Text="" Width ="180px" Visible="False"></asp:TextBox>
                                                                                                                   
                                                        <telerik:RadSlider ID="RadSliderA1" runat="server" AutoPostBack="true" MinimumValue="10" MaximumValue="40" OnValueChanged="RadSliderA1_ValueChanged" >
                                                        </telerik:RadSlider>

                                                        <div style="width: 180px; text-align: center; display:block; margin-left:5px ">
                                                            <asp:Button runat="server" Enabled="false" Width="160px" CssClass="btn btn-success" ID="btnEditPhotoA1" Text="Edit Photo for ID Card" OnClientClick="return openRadWindow(1);" Visible="true" />
                                                        </div>


                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="156px">Alternate 2 Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox runat="server" text="" ID="txtAlternate2" Visible="False" ></asp:TextBox>
                                                        </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <%--<asp:Image ID="Image1" runat="server" Height="154px" 
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />--%>
                                                        <img id="imgAlternate2" runat="server" src="~/Resource/Images/not-available.jpg" style="width: 210px; height: 236px" alt="" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>

                                                   <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Width="156px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">    </td>
                                                 <td style=" padding-left: 10px; padding-bottom: 20px; align-content: center;">
                                                        
                                                        <asp:DropDownList runat="server" ID="ddlBrightnessA2" Width="60px" Visible="false" >
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>1.1</asp:ListItem>
                                                            <asp:ListItem>1.2</asp:ListItem>
                                                            <asp:ListItem>1.3</asp:ListItem>
                                                            <asp:ListItem>1.4</asp:ListItem>
                                                            <asp:ListItem>1.5</asp:ListItem>
                                                            <asp:ListItem>1.6</asp:ListItem>
                                                            <asp:ListItem>1.7</asp:ListItem>
                                                            <asp:ListItem>1.8</asp:ListItem>
                                                            <asp:ListItem>1.9</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>2.1</asp:ListItem>
                                                            <asp:ListItem>2.2</asp:ListItem>
                                                            <asp:ListItem>2.3</asp:ListItem>
                                                            <asp:ListItem>2.4</asp:ListItem>
                                                            <asp:ListItem>2.5</asp:ListItem>
                                                            <asp:ListItem>2.6</asp:ListItem>
                                                            <asp:ListItem>2.7</asp:ListItem>
                                                            <asp:ListItem>2.8</asp:ListItem>
                                                            <asp:ListItem>2.9</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>3.1</asp:ListItem>
                                                            <asp:ListItem>3.2</asp:ListItem>
                                                            <asp:ListItem>3.3</asp:ListItem>
                                                            <asp:ListItem>3.4</asp:ListItem>
                                                            <asp:ListItem>3.5</asp:ListItem>
                                                            <asp:ListItem>3.6</asp:ListItem>
                                                            <asp:ListItem>3.7</asp:ListItem>
                                                            <asp:ListItem>3.8</asp:ListItem>
                                                            <asp:ListItem>3.9</asp:ListItem>
                                                            <asp:ListItem>4.0</asp:ListItem>

                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp; &nbsp;
                                                        <asp:Button runat="server" CssClass="btn btn-success" ID="btnChangeA2" Text="Change" OnClick="btnChangeA2_Click" Visible="false" />
                                                     
                                                        <asp:TextBox runat="server" ID="txtPhotoCardA2" Text="" Width ="180px" Visible="False"></asp:TextBox>

                                                      <telerik:RadSlider ID="RadSliderA2" runat="server" AutoPostBack="true" MinimumValue="10" MaximumValue="40" OnValueChanged="RadSliderA2_ValueChanged" >
                                                        </telerik:RadSlider>

                                                      <div style="width: 180px; text-align: center; display:block; margin-left:5px ">
                                                            <asp:Button runat="server" Enabled="false" Width="160px" CssClass="btn btn-success" ID="btnEditPhotoA2" Text="Edit Photo for ID Card" OnClientClick="return openRadWindow(2);" Visible="true" />
                                                        </div>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>

                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                           
                                            <asp:Button runat="server" CssClass="btn btn-warning" ID="btnPdfGenate" Text="Generate ID Card" OnClick="btnPdfGenate_Click" />

                                        </td>
                                        
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />

 

                            </div>
                        </div>
                    </div>
                </div>

    </div>
               
   
           <telerik:radwindowmanager id="RadWindowManager1" ClientIDMode="Static"  Width="890" Height="520" Modal="true" runat="server" DestroyOnClose="true"
                 >      
                 
                                    </telerik:radwindowmanager>        
    
            
                       
  </ContentTemplate>
  </asp:UpdatePanel>

                                                        <%--    <asp:Button runat="server" Visible="true"  CssClass="btn btn-success" ID="btnEditPhotoBeneficiary" Text="Edit" OnClientClick="return openRadWindow();" />--%>
        
           
    
   

 <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

