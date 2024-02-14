<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PiwikTest.aspx.cs" Inherits="SSSNP.PiwikTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>

     <!-- Piwik -->
    <script type="text/javascript">
        var _paq = _paq || [];
        _paq.push(['trackPageView']);
        _paq.push(['enableLinkTracking']);
        (function () {
            var u = "//";
            _paq.push(['setTrackerUrl', u + 'piwik.php']);
            _paq.push(['setSiteId', 120]);
            _paq.push(['setUserId', '<%= Username()  %>']);      
            var d = document, g = d.createElement('script'), s = d.getElementsByTagName('script')[0];
            g.type = 'text/javascript'; g.async = true; g.defer = true; g.src = u + 'piwik.js'; s.parentNode.insertBefore(g, s);
        })();
    </script>
    <noscript>
        <p>
            <img src="//" style="border: 0;" alt="" /></p>
    </noscript>
    <!-- End Piwik Code -->

</body>
</html>
