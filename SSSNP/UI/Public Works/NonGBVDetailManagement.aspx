<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NonGBVDetailManagement.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.NonGBVDetailManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script type="text/javascript" src="./assets/js/jquery.min.js"></script>
    <script src="./assets/js/fullcalendar.js"></script>
    <script type="text/javascript" src="./assets/js/popper.min.js"></script>
    <script type="text/javascript" src="./assets/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="./assets/js/jquery.mCustomScrollbar.concat.min.js"></script>
    <script type="text/javascript" src="./assets/js/jquery.dcjqaccordion.2.7.js"></script>
    <script type="text/javascript" src="./assets/js/jquery.validate.js"></script>
    <%--<script src="./assets/js/MEAPValidation.js"></script>--%>
    <link href="https://fonts.googleapis.com/css2?family=Noto+Sans" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Righteous&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" rel="stylesheet" />
    <link href="./assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="./assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="./assets/css/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="./assets/css/simple-line-icons.css" rel="stylesheet" type="text/css" />
    <link href="./assets/css/jquery.mCustomScrollbar.css" rel="stylesheet" />
    <link href="./assets/css/style.css" rel="stylesheet" />
    <link href="./assets/css/responsive.css" rel="stylesheet" />
    <link type="text/css" href="./assets/fonts/Meap_Fonts/styleFA.css" rel="stylesheet" />
    <link href="./assets/css/fullcalendar.css" rel='stylesheet' />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/js/bootstrap-datepicker.js"></script>
    <script src="https://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>

    <style type="text/css">
        .overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 1px;
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.9;
            -moz-opacity: 0.9;
        }

        .changeBrightnessButton {
            border: 1px solid #563d7c;
            border-radius: 5px;
            color: white;
            padding: 5px 10px 5px 25px;
            background: url(~/Resource/Images/brightnessIcon.png) left 3px top 5px no-repeat #563d7c;
        }

       .MyGridHeader {
            background-color: #dedede !important;
            font-family: Poppins;
            vertical-align: middle;
            /*background-image:none !important;*/
        }

        .MyGridAlternatingRow {
            font-family:Poppins;
            background-color: #D1DDF1 !important;
        }
        .RadGrid_Default .rgRow>td {
            font-family:Poppins;
            background-color: #fff;
            border-color: #fff;
            height: 35px;
        }

        div.RadGrid_Default .rgHeader, div.RadGrid_Default th.rgResizeCol .HeaderTitle {
            background-image: none;
            font-size: 12px !important;
            font-family: Poppins;
        }

            div.RadGrid_Default .rgHeader a {
                color: #333 !important;
                font-size: 12px !important;
                font-family: Poppins;
            }

            div.RadGrid_Default .rgHeader {
                color: #333 !important;
                font-size: 12px !important;
                font-family: Poppins;
            }
            a {
             color: #337ab7;
             text-decoration: none;
             padding: 0px;
            }
            .LinkButton {
             color: #333;
             border-color: #b3b3b3;
             background-image: linear-gradient(white,#e6e6e6);
             padding: 2px 9px;
             border-width: 1px;
             border-style: solid;
             border-color: #ccc;
             background-position: 0 0;
             border-radius: 4px;
            }
            .LinkButton:hover {
             border-color: #b3b3b3;
             background-image: linear-gradient(gainsboro,#c2c2c2);
            }
            /* Dashed red border */
            hr.blue {
              border-top: 2px dashed #337ab7;
            }

    </style>

    <script src="../../Resource/Styles/SideMenu/script.js" type="text/javascript"></script>

    <link href="../../Resource/Styles/SideMenu/styles.css" rel="stylesheet" type="text/css" />

    <script src="../Resource/Scripts/js/validation.js" type="text/javascript"></script>

    <script src="../../Resource/Scripts/js/jquery.js" type="text/javascript"></script>

    <script src="../../Resource/Scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Resource/Scripts/js/plugins/morris/raphael.min.js"></script>

    <script src="../../Resource/Scripts/js/plugins/morris/morris.min.js" type="text/javascript"></script>

    <script src="../../Resource/Scripts/js/plugins/morris/morris-data.js" type="text/javascript"></script>

    <link href="../../Resource/Styles/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Resource/Styles/css/sb-admin.css" rel="stylesheet" type="text/css" />
    <link href="../../Resource/Styles/css/plugins/morris.css" rel="stylesheet" type="text/css" />
    <link href="../../Resource/Styles/font-awesome/css/font-awesome.css" rel="stylesheet"
        type="text/css" />

    <style type="text/css">
       
        a {
         color: #337ab7;
         text-decoration: none;
         padding: 0px;
        }

        .style1 {
            width: 100%;
            max-height: 60px;
        }

        .style3 {
            width: 200px;
            background-color: #56bfe7;
        }

        .style4 {
            max-height: 60px;
         }

      
    </style>

    <style type="text/css">
        .classImage
        {
            background: url();
            width: 36px;
            height: 26px;
        }

    .classImage2{
             color: #333;
             border-color: #b3b3b3;
             background-image: linear-gradient(white,#e6e6e6);
             /*padding: 2px 9px;*/
             border-width: 1px;
             border-style: solid;
             border-color: #ccc;
             background-position: 0 0;
             border-radius: 4px;
             padding-top: 3px!important;
             padding-bottom: 3px!important;
             width: 36px;
             height: 26px;
    }
    </style>


    <script type="text/javascript">

 

     function GetRadWindow() {

            var oWindow = null;

            if (window.radWindow)

                oWindow = window.radWindow;

            else if (window.frameElement && window.frameElement.radWindow)

                oWindow = window.frameElement.radWindow;

            return oWindow;

        }

        function CloseModal() {

            // GetRadWindow().close();

            setTimeout(function () {

                GetRadWindow().close();

            }, 0);

        }

 

        function OnClientClicking(sender, args) {

            var callBackFunction = Function.createDelegate(sender, function (argument) {

                if (argument) {

                    this.click();

                }

            });

            var text = "Are you sure you want to delete the record?";

            radconfirm(text, callBackFunction, 300, 100, null, "Delete");

            args.set_cancel(true);

        }

 

    </script>

    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; padding-right: 9px; height: 575px;"
                        valign="top" align="center">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title" id="h3" runat="server">Non-GBV Detail
                                </h3>
                            </div>
                            <asp:UpdatePanel runat="server" ID="upPanelMain">
                                <ContentTemplate>

                                    <asp:HiddenField runat="server" ID="hfAttach_ActionResolution" Value="" />
                                   <asp:HiddenField runat="server" ID="hfAttach_ActionResolutionID" Value="0" />

                                   <asp:HiddenField runat="server" ID="hfAttach_UploadFeedback" Value="" />
                                   <asp:HiddenField runat="server" ID="hfAttach_UploadFeedbackID" Value="0" />
                                                                        
                                   <asp:HiddenField runat="server" ID="hfAttach_UploadInvestigation" Value="" />
                                   <asp:HiddenField runat="server" ID="hfAttach_UploadInvestigationID" Value="0" />

                                    <div class="panel-body">
                                        <table id="tbMain" width="100%" cellpadding="4" cellspacing="5">
                                            <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                      <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="128px">Action/s taken in response to the complaint</asp:Label>
                                                     <span style="text-align: left; color: red;">*</span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="73px" Width="100%" TextMode="MultiLine" Enabled="true" runat="server" ID="txtActionResponseComplaint" Text=""></asp:TextBox>
                                                    
                                                </td>
                                                

                                    

                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="128px">Action Taken by:</asp:Label>
                                                    <span style="text-align: left; color: red;">*</span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="133px" Width="100%" TextMode="MultiLine" Enabled="true" runat="server" ID="txtActionResolutionDesignation" Text=""></asp:TextBox>
                                                </td>
                                               
                                                            <td colspan="4" style="top: 0px; padding: 5px; margin: 5px; text-align: left;">
                                             Attachment:
                                                 <div>
                                                <asp:Panel runat="server" ID="pnlAttachActionResolution" Visible="true" Width="400px">
                                                    <asp:UpdatePanel ID="upAttachmentActionResolution" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:FileUpload runat="server" ID="fuActionResolution" Height="33px" Enabled="true" ClientIDMode="Static" Width="400px" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="BtnUploadActionResolution" />
                                                        </Triggers>

                                                    </asp:UpdatePanel>

                                                    <asp:Button runat="server" ID="BtnUploadActionResolution" Text="Upload" Enabled="true" CssClass="btn btn-warning" OnClick="BtnUploadActionResolution_Click"    />
                                                    &nbsp;
                                                           <asp:Button runat="server" ID="btnDownloadActionResolution" Text="Download" Enabled="false" CssClass="btn btn-success" OnClick="btnDownloadActionResolution_Click"   />

                                                    &nbsp;
                                                    <asp:Button ID="btnRemoveActionResolution" runat="server" CssClass="btn btn-danger" Height="32px" Enabled="false" OnClientClick="return confirm('Do you want to delete the attach ? ');" TabIndex="10" Text="Delete" OnClick="btnRemoveActionResolution_Click"  />
                                                    <br />
                                                    <asp:Label runat="server" ID="lblActionResolutionFilename" Text="" ForeColor="Red"></asp:Label>
                                                </asp:Panel>

                                            </div>


                                         </td>


                                            </tr>

                                            <tr>
                                                 <td style="padding: 5px; margin: 5px">
                                                    <asp:Label ID="Label13" runat="server" CssClass="LBLBLACK" Width="128px">Feedback to complainant</asp:Label>
                                                    <span style="text-align: left; color: red;">*</span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="133px" Width="100%" TextMode="MultiLine" Enabled="true" runat="server" ID="txtFeedbackComplainant" Text=""></asp:TextBox>
                                                </td>
                                                
                                                 <td colspan="4" style="top: 0px; padding: 5px; margin: 5px; text-align: left;">
                                             Attachment:
                                                 <div>
                                                <asp:Panel runat="server" ID="pnlAttachFeedback" Visible="true" Width="400px">
                                                    <asp:UpdatePanel ID="upFeedback" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:FileUpload runat="server" ID="fuFeedback" Height="33px" Enabled="true" ClientIDMode="Static" Width="400px" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="BtnUploadFeedback" />
                                                        </Triggers>

                                                    </asp:UpdatePanel>

                                                    <asp:Button runat="server" ID="btnUploadFeedback" Text="Upload" Enabled="true" CssClass="btn btn-warning" OnClick="btnUploadFeedback_Click"   />
                                                    &nbsp;
                                                           <asp:Button runat="server" ID="btnDownloadFeedback" Text="Download" Enabled="false" CssClass="btn btn-success" OnClick="btnDownloadFeedback_Click"   />

                                                    &nbsp;
                                                    <asp:Button ID="btnRemoveFeedback" runat="server" CssClass="btn btn-danger" Height="32px" Enabled="false" OnClientClick="return confirm('Do you want to delete the attach ? ');" TabIndex="10" Text="Delete" OnClick="btnRemoveFeedback_Click" />
                                                    <br />
                                                    
                                                    <asp:Label runat="server" ID="lblFeedbackFilename" Text="" ForeColor="Red"></asp:Label>
                                                </asp:Panel>

                                            </div>


                                         </td>

                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Width="128px">Details of the Accused</asp:Label>
                                                    <span style="text-align: left; color: red;"></span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="73px" Width="100%" TextMode="MultiLine" Enabled="true" runat="server" ID="txtDetailOfTheAccused" Text=""></asp:TextBox>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp; </td>
                                                <td style="padding: 5px; margin: 5px">
                                                    
                                                    Other</td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">
                                               
                                                    <asp:TextBox ID="txtDetailOfTheAccusedOther" runat="server" class="form-control txtbox" Enabled="true" Height="73px" Text="" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                               
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>

                                                  <tr>
                                        <td style="padding: 5px; margin: 5px">
                                             <asp:Label ID="Label21" runat="server" CssClass="LBLBLACK" Width="128px">Name of the Accused</asp:Label>
                                              <%--<span style="color: red;">*</span>--%>
                                            </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="txtAccusedName" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">
                                             
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px">
                                            
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>

                                            <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label16" runat="server" CssClass="LBLBLACK" Width="128px">State Name</asp:Label>
                                               <%--<span style="color: red;">*</span>--%>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlState" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp; </td>

                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label18" runat="server" CssClass="LBLBLACK" Width="128px">Payam/Block Name</asp:Label>
                                               <%--<span style="color: red;">*</span>--%>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Enabled="false" Height="33px" Width="250px" runat="server" ID="ddlPayam" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged"  >
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label19" runat="server" CssClass="LBLBLACK" Width="128px">County/Municipality Name</asp:Label>
                                               <%--<span style="color: red;">*</span>--%>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Width="250px" Enabled="false" runat="server" ID="ddlCounty" AutoPostBack="True" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged"  >
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label20" runat="server" CssClass="LBLBLACK" Width="128px">Boma/Quarter Council Name</asp:Label>
                                               <%--<span style="color: red;">*</span>--%>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Enabled="false" Width="250px" runat="server" ID="ddlBoma" AutoPostBack="True">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>

                                                 <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                    
                                                    <asp:Label ID="Label22" runat="server" CssClass="LBLBLACK" Width="128px">SNSOP Accused</asp:Label>
                                                    <%--<span style="text-align: left; color: red;">*</span>--%>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlSSSNPAccused" AutoPostBack="True" >
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                        <asp:ListItem Text="SNSOP Beneficiary" Value="SNSOP Beneficiary"></asp:ListItem>
                                                        <asp:ListItem Text="Committee Member" Value="Committee Member"></asp:ListItem>
                                                        <asp:ListItem Text="UNOPS personnel" Value="UNOPS personnel"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                                <td style="padding: 5px; margin: 5px">
                                                    
                                                </td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">
                                                   
                                                </td> 
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>


                                            <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="128px">Date of resolution</asp:Label>
                                                    <span style="text-align: left; color: red;">*</span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="txtDataOfResolution" Text="" OnTextChanged="txtDataOfResolution_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtAgeNumberFilter" runat="server" Enabled="True" TargetControlID="txtDataOfResolution" FilterType="Numbers, Custom" FilterMode="ValidChars" ValidChars="/"></cc1:FilteredTextBoxExtender>
                                                    <cc1:CalendarExtender ID="txtDateOfResolution_CalendarExtender" OnPreRender="txtDateOfResolution_CalendarExtender_PreRender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDataOfResolution"></cc1:CalendarExtender>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                <td style="padding: 5px; margin: 5px">
                                                      

                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                    
                                                    <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="128px">Pending Action Required</asp:Label>
                                                    <span style="text-align: left; color: red;">*</span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlPendingActionRequered" AutoPostBack="True" OnSelectedIndexChanged="ddlPendingActionRequered_SelectedIndexChanged">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                                <td style="padding: 5px; margin: 5px">
                                                    
                                                </td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">
                                                   
                                                </td> 
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>

                                                  <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                    
                                                    <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="128px">Pending Action Details</asp:Label>
                                                    <span style="text-align: left; color: red;"></span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                  <asp:TextBox class="form-control txtbox" Height="73px" Width="100%" TextMode="MultiLine" Enabled="false" runat="server" ID="txtPendingActionDetail" Text=""></asp:TextBox>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                                <td style="padding: 5px; margin: 5px">
                                                    
                                                     <asp:Label ID="Label11" runat="server" CssClass="LBLBLACK" Width="128px">Escalated/Referred externally</asp:Label>
                                                    <span style="text-align: left; color: red;">*</span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlEscaletedReferredExternally" AutoPostBack="True" OnSelectedIndexChanged="ddlEscaletedReferredExternally_SelectedIndexChanged">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>

                                            <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                    
                                                     <asp:Label ID="Label8" runat="server" CssClass="LBLBLACK" Width="128px">Appeal Lodged?</asp:Label>
                                                    <span style="text-align: left; color: red;">*</span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlAppealLodged" AutoPostBack="True" OnSelectedIndexChanged="ddlAppealLodged_SelectedIndexChanged">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                                <td style="padding: 5px; margin: 5px">Appeal Date</td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" Enabled="false" runat="server" ID="txtAppealDate" Text=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtAppealDate_filterExtender" runat="server" Enabled="True" TargetControlID="txtAppealDate" FilterType="Numbers, Custom" FilterMode="ValidChars" ValidChars="/"></cc1:FilteredTextBoxExtender>
                                                    <cc1:CalendarExtender ID="txtAppealDate_CalendarExtender" OnPreRender="txtAppealDate_CalendarExtender_PreRender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtAppealDate"></cc1:CalendarExtender>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px">Appeal Outcome Satisfactory</td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlAppealOutcomeSatisfactory" AutoPostBack="True" OnSelectedIndexChanged="ddlAppealOutcomeSatisfactory_SelectedIndexChanged" Enabled="False">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                                <td style="padding: 5px; margin: 5px">Recommended course of action</td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="73px" Width="400px" TextMode="MultiLine" Enabled="false" runat="server" ID="txtUnresolvedReccommended" Text=""></asp:TextBox>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px">Was an investigation carried out? </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlInvestigation" AutoPostBack="True" OnSelectedIndexChanged="ddlInvestigation_SelectedIndexChanged">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                     <asp:Label ID="Label15" runat="server" CssClass="LBLBLACK" Width="128px">Name</asp:Label>
                                                    <span style="text-align: left; color: red;">*</span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="33px" Width="400px" TextMode="SingleLine" Enabled="false" runat="server" ID="txtNameInvestigation" Text=""></asp:TextBox>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                                 <td style="padding: 5px; margin: 5px">
                                                     
                                                    <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="128px">Designation</asp:Label>
                                                    <span style="text-align: left; color: red;">*</span>
                                                 </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="33px" Width="400px" TextMode="SingleLine" Enabled="false" runat="server" ID="txtDesignation" Text=""></asp:TextBox>
                                               
                                                 </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>

                                            <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                     <asp:Label ID="Label14" runat="server" CssClass="LBLBLACK" Width="128px">Date Investigation was Completed</asp:Label>
                                                    <span style="text-align: left; color: red;">*</span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" Enabled="false" runat="server" ID="txtDataInvestigation" Text=""></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtDataInvestigation" FilterType="Numbers, Custom" FilterMode="ValidChars" ValidChars="/"></cc1:FilteredTextBoxExtender>
                                                    <cc1:CalendarExtender ID="CalDateInvestigation" OnPreRender="CalDateInvestigation_PreRender" Enabled="false" runat="server" Format="dd/MM/yyyy"  TargetControlID="txtDataInvestigation"></cc1:CalendarExtender>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                                 <td style="padding: 5px; margin: 5px">
                                                     
                                                    <asp:Label ID="Label12" runat="server" CssClass="LBLBLACK" Width="128px">Main findings</asp:Label>
                                                    <span style="text-align: left; color: red;">*</span>
                                                 </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="33px" Width="400px" TextMode="SingleLine" Enabled="false" runat="server" ID="txtMainFindings" Text=""></asp:TextBox>
                                               
                                                 </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>

                                                          
                                                              
                                                                   <tr>
                                                                       <td style="padding: 5px; margin: 5px">
                                                                           <asp:Label ID="Label17" runat="server" CssClass="LBLBLACK" Width="128px"> What Action was Taken?</asp:Label>
                                                                           <span style="text-align: left; color: red;"></span></td>
                                                                       <td style="padding: 5px; margin: 5px">:</td>
                                                                       <td style="padding: 5px; margin: 5px">
                                                                           <asp:TextBox ID="txtInvestigationAction" runat="server" class="form-control txtbox" Enabled="true" Height="133px" Text="" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                                       </td>
                                                                       <td colspan="4" style="top: 0px; padding: 5px; margin: 5px; text-align: left;">
                                                                           <div>
                                                                               Attach an Investigation Report
                                                                               <asp:Panel ID="pnlInvestigation" runat="server" Visible="true" Width="400px">
                                                                                   <asp:UpdatePanel ID="upInvestigation" runat="server" UpdateMode="Conditional">
                                                                                       <ContentTemplate>
                                                                                           <asp:FileUpload ID="fuInvestigation" runat="server" ClientIDMode="Static" Enabled="true" Height="33px" Width="400px" />
                                                                                       </ContentTemplate>
                                                                                       <triggers>
                                                                                           <asp:PostBackTrigger ControlID="BtnUploadInvestigation" />
                                                                                       </triggers>
                                                                                   </asp:UpdatePanel>
                                                                                   <asp:Button ID="BtnUploadInvestigation" runat="server" CssClass="btn btn-warning" Enabled="true" OnClick="BtnUploadInvestigation_Click" Text="Upload" />
                                                                                   &nbsp;
                                                                                   <asp:Button ID="BtnDownloadInvestigation" runat="server" CssClass="btn btn-success" Enabled="false" OnClick="BtnDownloadInvestigation_Click" Text="Download" />
                                                                                   &nbsp;
                                                                                   <asp:Button ID="BtnRemoveInvestigation" runat="server" CssClass="btn btn-danger" Enabled="false" Height="32px" OnClick="BtnRemoveInvestigation_Click" OnClientClick="return confirm('Do you want to delete the attach ? ');" TabIndex="10" Text="Delete" />
                                                                                   <br />
                                                                                   <asp:Label ID="lblInvestigationFilename" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                                               </asp:Panel>
                                                                           </div>
                                                                       </td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td style="padding: 5px; margin: 5px">Closing Date after appeal</td>
                                                                       <td style="padding: 5px; margin: 5px">:</td>
                                                                       <td style="padding: 5px; margin: 5px">
                                                                           <asp:TextBox ID="txtClosingDate" runat="server" class="form-control txtbox" Enabled="False" Height="33px" Text="" Width="180px"></asp:TextBox>
                                                                           <cc1:FilteredTextBoxExtender ID="txtClosingDate_FilterExtender" runat="server" Enabled="True" FilterMode="ValidChars" FilterType="Numbers, Custom" TargetControlID="txtClosingDate" ValidChars="/" />
                                                                           <cc1:CalendarExtender ID="txtClosingDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" OnPreRender="txtClosingDate_CalendarExtender_PreRender" TargetControlID="txtClosingDate" />
                                                                       </td>
                                                                       <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                       <td style="padding: 5px; margin: 5px">
                                                                           <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK" Width="128px">Status</asp:Label>
                                                                           <span style="text-align: left; color: red;">*</span> </td>
                                                                       <td style="padding: 5px; margin: 5px">:</td>
                                                                       <td style="padding: 5px; margin: 5px">
                                                                           <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" class="form-control dropdown" Height="33px" Width="180px">
                                                                               <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                           </asp:DropDownList>
                                                                       </td>
                                                                       <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                   </tr>
                                                                   <tr>
                                                                       <td style="padding: 5px; margin: 5px"></td>
                                                                       <td style="padding: 5px; margin: 5px"></td>
                                                                       <td style="text-align: left; padding: 5px; margin: 5px"><i>Attachment(s) can be uploaded only after detail has been saved</i> </td>
                                                                       <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                       <td style="padding: 5px; margin: 5px"></td>
                                                                       <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                       <td style="padding: 5px; margin: 5px"></td>
                                                                       <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                   </tr>
                                                                   <asp:UpdatePanel ID="upAttachment" runat="server" UpdateMode="Conditional">
                                                                       <ContentTemplate>
                                                                           <tr>
                                                                               <td style="padding: 5px; margin: 5px">Attachment</td>
                                                                               <td style="padding: 5px; margin: 5px">:</td>
                                                                               <td style="padding: 5px; margin: 5px">
                                                                                   <asp:FileUpload ID="upFile" runat="server" ClientIDMode="Static" Height="33px" Width="400px" />
                                                                               </td>
                                                                               <td colspan="4" style="padding: 5px; margin: 5px">
                                                                                   <asp:Label ID="Label5" runat="server" Font-Italic="true" ForeColor="Green" Text=""></asp:Label>
                                                                               </td>
                                                                               <%--<td style="padding: 5px; margin: 5px">
                                            </td>
                                         <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">
                                           <asp:Label runat="server" ID="lblFilename" Text="Filename" Font-Italic="true" ForeColor="Green"></asp:Label>
                                                                                     </td>--%>
                                                                               <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                           </tr>
                                                                           <tr>
                                                                               <td style="padding: 5px; margin: 5px"></td>
                                                                               <td style="padding: 5px; margin: 5px">:</td>
                                                                               <td style="padding: 5px; margin: 5px; text-align: left;">
                                                                                   <asp:Button ID="btnUploadFile" runat="server" CssClass="btn btn-warning" OnClick="btnUploadFile_Click" Text="Upload File" />
                                                                               </td>
                                                                               <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                               <td style="padding: 5px; margin: 5px"></td>
                                                                               <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                               <td style="padding: 5px; margin: 5px"></td>
                                                                               <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                           </tr>
                                                                       </ContentTemplate>
                                                                       <triggers>
                                                                           <asp:PostBackTrigger ControlID="btnUploadFile" />
                                                                       </triggers>
                                                                   </asp:UpdatePanel>
                                                               
                                            </tr>
                                        </table>
                                        <hr class="blue" />
                                        <table id="tbGridAttachment" width="95%" cellpadding="1" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <telerik:RadGrid ID="RadGridAttachment" AutoGenerateColumns="false" runat="server" Visible="true"
                                                        AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both"
                                                        MasterTableView-TableLayout="Fixed"
                                                        OnNeedDataSource="RadGridAttachment_NeedDataSource" OnItemDataBound="RadGridAttachment_ItemDataBound">
                                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                        <MasterTableView TableLayout="Fixed"
                                                            DataKeyNames="NonGBVAttachmentID"
                                                            ClientDataKeyNames="NonGBVAttachmentID">
                                                            <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                            <%--<AlternatingItemStyle CssClass="MyGridAlternatingRow" />--%>
                                                            
                                                            <Columns>
                                                                <telerik:GridBoundColumn DataField="NonGBVAttachmentID" HeaderText="NonGBVAttachmentID" HeaderStyle-Width="15%" Visible="false"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Filename" HeaderText="Filename" HeaderStyle-Width="70%"></telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="DownloadColumn"
                                                                      HeaderStyle-Width="100px" ItemStyle-Width="150px">
                                                                    <ItemTemplate>
                                                                          <asp:LinkButton runat="server"  ID="btnDownload" OnClick="btnDownload_Click" CssClass="LinkButton">
                                                                                                                                            <i class="fa fa-arrow-down"  style="color:#333333"></i>
                                                                            </asp:LinkButton>
                                                                                                                            
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                                                                       

                                                                <telerik:GridTemplateColumn UniqueName="DeleteColumn" HeaderText="" HeaderStyle-Width="100px" ItemStyle-Width="150px" >
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Do you want to delete the record ? '); "  OnClick="btnDelete_Click" CssClass="LinkButton" >
                                                                                                                                       <i class="fa fa-trash" style="color:#333333"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="padding: 5px; margin: 5px">
                                                    <div id="DivSuccess" runat="server" class="alert alert-success" visible="false">
                                                        <a aria-label="close" class="close" data-dismiss="alert" href="#"></a> <strong>Success!</strong><asp:Label ID="lblMessageSuccess" runat="server"></asp:Label>
                                                    </div>
                                                    <div id="DivFailure" runat="server" class="alert alert-danger" visible="false">
                                                        <a aria-label="close" class="close" data-dismiss="alert" href="#"></a> <strong>Warning!</strong>
                                                        <asp:Label ID="lblMessageError" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="padding: 20px" align="center">
                                            <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-success" Height="32px" TabIndex="10" Text="Save" Width="120px" OnClick="BtnSave_Click" />
                                            &nbsp; &nbsp; 
                                        <asp:Button ID="BtnClose" runat="server" CssClass="btn btn-primary" Height="32px" TabIndex="10" Text="Close" Width="120px" OnClick="BtnClose_Click" />
                                            &nbsp; &nbsp;                                                 
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    
                </tr>
            </table>


    </form>
</body>
</html>
