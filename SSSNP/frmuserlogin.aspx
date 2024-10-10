<%@ Page Language="C#" AutoEventWireup="true" Inherits="index" CodeBehind="frmUserLogin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="Realist Responsive web template, Bootstrap Web Templates, Flat Web Templates, Andriod Compatible web template, Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyErricsson, Motorola web design" />

    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <%--    
    <a href="Resource/Styles/Site/fonts/fontawesome-webfont.ttf"></a>
    <a href="Resource/Styles/Site/fonts/FontAwesome.otf"></a>--%>
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

    <link href="https://fonts.googleapis.com/css2?family=Montserrat" rel="stylesheet" />


    <script type="text/javascript">
        $(document).ready(function () {
            $('#horizontalTab').easyResponsiveTabs({
                type: 'default', //Types: default, vertical, accordion           
                width: 'auto', //auto or any width like 600px
                fit: true   // 100% fit in a container
            });
        });
    </script>



    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-128375426-1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-128375426-1');
    </script>

    <style>
        @import "compass/css3";

        * {
            box-sizing: border-box;
        }

        p {
            margin: 0px;
        }

        body {
            font-family: Montserrat;
            color: grey;
            font-size: 14px;
            position: absolute;
            background-color: #FFF /*#009EDB;*/;
            background-image: url('Resource/Images/Background_cover.png');
            background-size: cover;
            background-position: center top;
            background-attachment: inherit;
            background-repeat: no-repeat;
            display: flex;
            flex-direction: column;
            justify-content: center;
            z-index: -9;
            width: 100%;
            top: 215px;
            left: -28px;
        }

        li {
            list-style-type: square;
            padding: 10px 20px 10px 20px;
            background-size: 60px 80px;
            text-align: center;
        }

        .header-btn {
            color: #fff;
            background: url("resource/images/Home.png") no-repeat scroll 0 0 transparent;
            font-family: Montserrat;
            width: 40px;
            height: 40px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 5px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }

            .header-btn:hover, header-btn:focus, header-btn:active {
                /*border: 1px solid #fff;
    border-radius: 8px;
    box-shadow: 0 7px 14px rgba(50, 50, 93, 0.1), 0 3px 6px rgba(0, 0, 0, 0.08);*/
                width: 45px;
                height: 45px;
                border-radius: 50%;
                -webkit-border-radius: 50%;
                -moz-border-radius: 50%;
                box-shadow: 0 0 5px rgba(0, 0, 0, .9);
                -webkit-box-shadow: 0 0 5px rgba(0, 0, 0, .9);
                -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .9);
            }

        .home-btn {
            background: url(Resource/Images/Home.png) no-repeat;
            font-family: Montserrat;
            width: 45px;
            height: 45px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }

            .home-btn:hover, home-btn:focus, home-btn:active {
                box-shadow: 0 7px 7px rgba(50, 50, 93, 0.1);
                color: transparent;
                background: url(Resource/Images/Home_button_hover.png) no-repeat;
                width: 45px;
                height: 45px;
                border-radius: 50%;
                -webkit-border-radius: 50%;
                -moz-border-radius: 50%;
            }

        .about-btn {
            background: url(Resource/Images/About.png) no-repeat;
            font-family: Montserrat;
            width: 45px;
            height: 45px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }

            .about-btn:hover, about-btn:focus, home-btn:active {
                box-shadow: 0 7px 7px rgba(50, 50, 93, 0.1);
                color: transparent;
                background: url(Resource/Images/About_button_hover.png) no-repeat;
                width: 45px;
                height: 45px;
                border-radius: 50%;
                -webkit-border-radius: 50%;
                -moz-border-radius: 50%;
            }

        .progress-btn {
            background: url(Resource/Images/Progress.png) no-repeat;
            font-family: Montserrat;
            width: 45px;
            height: 45px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }

            .progress-btn:hover, progress-btn:focus, progress-btn:active {
                box-shadow: 0 7px 7px rgba(50, 50, 93, 0.1);
                color: transparent;
                background: url(Resource/Images/Progress_button_hover.png) no-repeat;
                width: 45px;
                height: 45px;
                border-radius: 50%;
                -webkit-border-radius: 50%;
                -moz-border-radius: 50%;
            }

        .gallery-btn {
            background: url(Resource/Images/Gallery.png) no-repeat;
            font-family: Montserrat;
            width: 45px;
            height: 45px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }

            .gallery-btn:hover, gallery-btn:focus, gallery-btn:active {
                box-shadow: 0 7px 7px rgba(50, 50, 93, 0.1);
                color: transparent;
                background: url(Resource/Images/Gallery_button_hover.png) no-repeat;
                width: 45px;
                height: 45px;
                border-radius: 50%;
                -webkit-border-radius: 50%;
                -moz-border-radius: 50%;
            }

        .contacts-btn {
            background: url(Resource/Images/Contacts.png) no-repeat;
            font-family: Montserrat;
            width: 45px;
            height: 45px;
            overflow: hidden;
            border-radius: 50%;
            -webkit-border-radius: 50%;
            -moz-border-radius: 50%;
            box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -webkit-box-shadow: 0 0 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0 0 5px rgba(0, 0, 0, .5);
        }

            .contacts-btn:hover, contacts-btn:focus, contacts-btn:active {
                box-shadow: 0 7px 7px rgba(50, 50, 93, 0.1);
                color: transparent;
                background: url(Resource/Images/Contacts_button_hover.png) no-repeat;
                width: 45px;
                height: 45px;
                border-radius: 50%;
                -webkit-border-radius: 50%;
                -moz-border-radius: 50%;
            }

        .btn-SignIn {
            color: #fff;
            font-family: Montserrat;
            font-size: 20px;
            width: 150px;
            margin-left: 0px;
            padding-top: 0px;
            padding-bottom: 0px;
            border-color: transparent;
            border-radius: 12px;
            background: linear-gradient(180deg, rgba(10,107,204,1) 40%, rgba(0,158,219,1) 95%, rgba(10,107,204,1) 100%);
        }

            .btn-SignIn:hover, .btn-SignIn:focus, .btn-SignIn:active {
                transform: translate(0,-1px);
                color: #fff;
                background: linear-gradient(180deg, rgba(10,107,204,1) 100%, rgba(0,158,219,1) 85%, rgba(0,158,219,1) 100%);
                box-shadow: 0 7px 14px rgba(50, 50, 93, 0.1), 0 3px 6px rgba(0, 0, 0, 0.08);
                text-decoration: none;
                cursor: pointer;
            }

        .login-Laptop6 {
            vertical-align: bottom;
            height: 100%;
            width: 100%;
        }

        .justify-content-md-end {
            -ms-flex-pack: end !important;
            justify-content: flex-end !important;
        }

        .d-md-flex {
            display: -ms-flexbox !important;
            display: flex !important;
        }

        .breadcrumb_nav {
            margin-bottom: 15px;
            align-self: flex-end;
        }

        .container_LoginPage {
            margin-top: 10px;
            position: relative;
        }
    </style>

</head>

<body>

    <form runat="server">
        <div class="container_LoginPage ">


            <div class="container">
                <div class="row">
                    <div class="col-md" style="margin-top: 100px; align-self: center;">
                    </div>
                    <%-- <div class="col-md-6 justify-content-md-end d-md-flex" style="margin-top: 80px; align-content:flex-end ">
						<div class="breadcrumb_nav" style="margin-bottom: 0px; align-self: flex-end; color: #616161; width:600px;">
							<ol class="breadcrumb" style="flex-wrap: nowrap;">
                            <li style="padding: 20px;"><button class="btn home-btn"></button> <br /> <strong>Home</strong> </li>
                            <li style="padding: 20px;"><button class="btn about-btn"></button>  <br /> <strong>About</strong> </li>
                            <li style="padding: 20px;"><button class="btn progress-btn"></button> <br /> <strong>Progress</strong> </li>
                            <li style="padding: 20px;"><button class="btn gallery-btn"></button> <br /> <strong>Gallery</strong> </li>
                            <li style="padding: 20px;"><button class="btn contacts-btn"></button> <br /> <strong>Contacts</strong> </li>
	                        </ol>
						</div>
					</div>
                    --%>
                </div>

                <div class="row" style="padding-top: 50px;">
                    <!-- 300px -->
                    <div class="col-md" style="margin-bottom: 50px;">
                        <div class="btn-group dropup mb-6">
                            <div class="cover-title">
                                <img src="Resource/Images/SNSOP_logo.png" alt="" style="height: 100px; display: inline-block; vertical-align: middle;" />
                                
                                <p style="font-family: Montserrat; font-size: 30pt; line-height: 60px; padding: 10px 0px 30px 0px; max-width: 360px; color: #616161; font-weight: bold;">
                                    Management<br />
                                    Information
                                    <br />
                                    System</p>

                                <asp:Button runat="server" ID="btnSignIn" class="btn btn-SignIn" onserverclick="btnSignIn_ServerClick" Text="Login" OnClick="btnSignIn_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md" style="align-self: center; padding-top: 2px;">
                        <div class="btn-group dropup mb-6">

                            <img src="Resource/Images/Image_optimized_100.png" class="login-Laptop6" alt="" aria-atomic="False" />
                        </div>
                        <br />
                        <img src="Resource/Images/SNSOP_logo_transparent.png" alt="" style="height: 70px; display: inline-block; vertical-align: middle;" />
                    </div>
                </div>



                <%--<div class="row"><br /></div>--%>
                <div class="row">
                    <br />
                </div>

            </div>
        </div>
        <asp:HiddenField runat="server" ID="cntl" />

    </form>

    <script type="text/javascript" src="../assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="../assets/js/popper.min.js"></script>
    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../assets/js/jquery.dcjqaccordion.2.7.js"></script>
    <script src="../assets/js/custom.js" type="text/javascript"></script>


    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-128375426-1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-128375426-1');
    </script>


    <%--  <td>
                        &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>--%>

    <footer class="page-footer">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12">
                    &nbsp;
 
                </div>
            </div>
        </div>
        <div class="footer-copyright">
            <div class="container-fluid text-left">
                <div class="row">

                </div>
            </div>
        </div>
    </footer>

</body>

</html>


