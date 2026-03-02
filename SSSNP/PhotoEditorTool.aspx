<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoEditorTool.aspx.cs" Inherits="SSSNP.PhotoEditorTool" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <script type="text/javascript">
            function modifyCommand(imageEditor, args) {
                if (args.get_commandName() == "Crop") {
                    waitForCommand(imageEditor, args.get_commandName(), function (widget) {
                        var width = 120;
                        var height = 136;
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
            function waitForCommand(imageEditor, commandName, callback) {
                var timer = setInterval(function () {
                    var widget = imageEditor.get_currentToolWidget();
                    if (widget && widget.get_name() == commandName) {
                        clearInterval(timer);
                        callback(widget);
                    }
                }, 100);
           }

           (function (global, undefined) {
               function OnClientCommandExecuting(imEditor, eventArgs) {
                   if (eventArgs.get_commandName() == 'Save') {
                       imEditor.saveImageOnServer('', true);

                       //Prevent the buil-in Save dialog to pop up
                       imEditor.setToggleState('Save', false);
                       imEditor.saveImageOnServer('', true);
                       eventArgs.set_cancel(true);
                   }
               }

               function OnClientSaved(imgEditor, args) {
                   GetRadWindow().close();
               }

               global.OnClientSaved = OnClientSaved;
               global.OnClientCommandExecuting = OnClientCommandExecuting;
           })(window);

           function GetRadWindow() {
               var oWindow = null;
               if (window.radWindow)
                   oWindow = window.radWindow;
               else if (window.frameElement && window.frameElement.radWindow)
                   oWindow = window.frameElement.radWindow;
               return oWindow;
           }


    </script>     
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

        <div>
            <h3>PhotoEditor Tool</h3>
            
            <telerik:RadImageEditor RenderMode="Lightweight" runat="server" ID="RadImageEditor1" Width="400" Height="350"  
               OnClientCommandExecuted="modifyCommand"  OnImageSaving="RadImageEditor1_ImageSaving"   OnClientSaved="OnClientSaved"
                ToolsLoadPanelType="RadAjaxPanel" OnClientCommandExecuting="OnClientCommandExecuting"  > 
               <Tools> 
                   
                    <telerik:ImageEditorToolGroup> 
                         <telerik:ImageEditorTool Text="Save" CommandName="Save"  /> 
                         <telerik:ImageEditorTool Text="Crop" CommandName="Crop" /> 
                        
                    </telerik:ImageEditorToolGroup> 
              </Tools>
            </telerik:RadImageEditor>
           
          
        </div>
    </form>
</body>
</html>
