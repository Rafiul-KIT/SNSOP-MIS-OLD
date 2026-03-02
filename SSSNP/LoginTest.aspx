<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginTest.aspx.cs" Inherits="SSSNP.LoginTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">



    <title></title>



    <style>
        .btn-SignIn {     color: #fff;     font-family: Montserrat;     font-size: 14px;     width: 90px;     margin-left: 5px;     padding-top: 0px;     padding-bottom: 0px;     border-color: transparent;     border-radius: 12px;     background: linear-gradient(180deg, rgba(10,107,204,1) 40%, rgba(0,158,219,1) 95%, rgba(10,107,204,1) 100%); } .btn-SignIn:hover, .btn-SignIn:focus, .btn-SignIn:active {     transform: translate(0,-1px);     color: #fff;     background: rgb(0,158,219,1);     background: linear-gradient(180deg, rgba(10,107,204,1) 100%, rgba(0,158,219,1) 85%, rgba(0,158,219,1) 100%);     box-shadow: 0 7px 14px rgba(50, 50, 93, 0.1), 0 3px 6px rgba(0, 0, 0, 0.08);     text-decoration: none;     cursor: pointer; } .login-Laptop6 {     horizontal-align: right;     height: 100%;     width: 100%; } justify-content-md-end{     -ms-flex-pack:end!important;     justify-content:flex-end!important
} .container_LoginPage {     margin-top: 10px;     position: relative; }
        li {
            list-style-type: none;
            padding: 5px 10px 5px 10px;
        }

        body {
           
            font-size: 14px;
/*            position: absolute;*/
            background-color: #fff;
            background-image: url(Resource/Images/Background-01.png);
            background-position: center top;
            background-attachment: inherit;
            background-size: 1920px 750px;
            background-repeat: no-repeat;
            display: flex;
            flex-direction: column;
            justify-content: center;
            z-index: -9;
            width: 100%;
        }.home-btn {
            color: grey;
            background: url(Resource/Images/Home.png) no-repeat;
            font-family: Montserrat;
            width: 47px;
            height: 47px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }
.home-btn:hover, home-btn:focus, home-btn:active {
    box-shadow: 0 7px 14px rgba(50, 50, 93, 0.1), 0 3px 6px rgba(0, 0, 0, 0.08);
    color: transparent;
    background: url(Resource/Images/Home_button_hover.png) no-repeat;
    width: 48px;
    height: 48px;
    border-radius: 50%;
    -webkit-border-radius: 50%;
    -moz-border-radius: 50%;
    box-shadow: 0 0 5px rgba(0, 0, 0, .9);
    -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .9);
    -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .9);
}
.about-btn {
            color: grey;
            background: url(Resource/Images/About.png) no-repeat;
            font-family: Montserrat;
            width: 47px;
            height: 47px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }
.about-btn:hover, about-btn:focus, home-btn:active {
    box-shadow: 0 7px 14px rgba(50, 50, 93, 0.1), 0 3px 6px rgba(0, 0, 0, 0.08);
    color: transparent;
    background: url(Resource/Images/About_button_hover.png) no-repeat;
    width: 48px;
    height: 48px;
    border-radius: 50%;
    -webkit-border-radius: 50%;
    -moz-border-radius: 50%;
    box-shadow: 0 0 5px rgba(0, 0, 0, .9);
    -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .9);
    -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .9);
}
.progress-btn {
            color: grey;
            background: url(Resource/Images/Progress.png) no-repeat;
            font-family: Montserrat;
            width: 47px;
            height: 47px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }
.progress-btn:hover, progress-btn:focus, progress-btn:active {
    box-shadow: 0 7px 14px rgba(50, 50, 93, 0.1), 0 3px 6px rgba(0, 0, 0, 0.08);
    color: transparent;
    background: url(Resource/Images/Progress_button_hover.png) no-repeat;
    width: 48px;
    height: 48px;
    border-radius: 50%;
    -webkit-border-radius: 50%;
    -moz-border-radius: 50%;
    box-shadow: 0 0 5px rgba(0, 0, 0, .9);
    -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .9);
    -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .9);
}
.gallery-btn {
            color: grey;
            background: url(Resource/Images/Gallery.png) no-repeat;
            font-family: Montserrat;
            width: 47px;
            height: 47px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }
.gallery-btn:hover, gallery-btn:focus, gallery-btn:active {
    box-shadow: 0 7px 14px rgba(50, 50, 93, 0.1), 0 3px 6px rgba(0, 0, 0, 0.08);
    color: transparent;
    background: url(Resource/Images/Gallery_button_hover.png) no-repeat;
    width: 48px;
    height: 48px;
    border-radius: 50%;
    -webkit-border-radius: 50%;
    -moz-border-radius: 50%;
    box-shadow: 0 0 5px rgba(0, 0, 0, .9);
    -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .9);
    -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .9);
}
.contacts-btn {
            color: grey;
            background: url(Resource/Images/Contacts.png) no-repeat;
            font-family: Montserrat;
            width: 47px;
            height: 47px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }
.contacts-btn:hover, contacts-btn:focus, contacts-btn:active {
    box-shadow: 0 7px 14px rgba(50, 50, 93, 0.1), 0 3px 6px rgba(0, 0, 0, 0.08);
    color: transparent;
    background: url(Resource/Images/Contacts_button_hover.png) no-repeat;
    width: 48px;
    height: 48px;
    border-radius: 50%;
    -webkit-border-radius: 50%;
    -moz-border-radius: 50%;
    box-shadow: 0 0 5px rgba(0, 0, 0, .9);
    -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .9);
    -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .9);
}


</style>
</head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="Realist Responsive web template, Bootstrap Web Templates, Flat Web Templates, Andriod Compatible web template, Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyErricsson, Motorola web design" />



        <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    
    <a href="Resource/Styles/Site/fonts/fontawesome-webfont.ttf"></a>
    <a href="Resource/Styles/Site/fonts/FontAwesome.otf"></a>
    <%--<link href="Resource/Styles/Site/font-awesome.css" rel="stylesheet" type="text/css" />--%>
    <link href="Resource/Styles/Site/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Resource/Styles/Site/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <script src="Resource/Styles/Site/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="Resource/Styles/Site/responsiveslides.min.js" type="text/javascript"></script>
    <script src="Resource/Styles/Site/easyResponsiveTabs.js" type="text/javascript"></script>

    <link href="Resource/Styles/Site/style.css" rel="stylesheet" type="text/css" />
    <link href="Resource/Styles/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
 
    
    <!-- google font -->
    <link href="https://fonts.googleapis.com/css?family=Comfortaa:300,400,500,600,700" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/simple-line-icons.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/jquery.mCustomScrollbar.css" rel="stylesheet" />
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../assets/css/responsive.css" rel="stylesheet" />
 
    <link href="https://fonts.googleapis.com/css2?family=Montserrat" rel="stylesheet"/>

<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
