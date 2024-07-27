<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportsMNE.aspx.cs" Inherits="SSSNP.UI.Public_Works.ReportsMNE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <style>
		    @import "compass/css3";        

            * { box-sizing: border-box; }
            p {
                margin: 0px;
            }

            body {
                font-family: Poppins;
                color: white;
                font-size: 14px;
                background-color: #fff;
                background-image: url(/Resource/Images/Background_cover.png);
                background-position: center top;
                background-attachment: inherit;
                background-size: cover;
                background-repeat: no-repeat;
                margin-left: 50px;
                display: flex;
                z-index: -9;
                width: 100%;
            }
            .modal-content {
                background-color: #1d1d1d;
            }
            .row {
               display: -ms-flexbox;
               display: flex;
               -ms-flex-wrap: wrap;
               flex-wrap: wrap;
               margin-right: 0px;
               margin-left: 0px
}
            .textGradient {
                color: #ff8c00; /* fix for not webkit browsers */
                background: linear-gradient(180deg, rgba(10,107,204,1) 40%, rgba(0,158,219,1) 95%, rgba(10,107,204,1) 100%);
                background-size: cover;
                -webkit-background-clip: text;
               -webkit-text-fill-color: transparent;
                background-clip: text;
               text-fill-color: transparent;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
 <div class="row" style="padding-top: 50px;"> <!-- 300px -->
                <div class="col-md" style="margin-bottom: 50px;">
                   <div class="btn-group dropup mb-6">
                       <div class="cover-title"> 
                        <p class="textGradient" style="font-family:Righteous; margin-top:30%; font-size: 60pt; padding-left: 7px; line-height: 110px;">Ooops!</p>
                        <p style="font-family:Poppins; font-size: 16pt; color:#0A6BCC; padding: 20px 0 20px 7px; max-width: 700px;">
                            Working on it.
                        </p>
                          </div>
                    </div>	
                </div>    
               <div class="right_detail" style="align-self: flex-end; padding-top: 2px;">
                      <div class="btn-group dropup mb-6"><img src="Resource/Images/Error Page.png" style="max-width: 800px;"/> </div>
                </div>
           </div>
</form>
</body>
</html>
