using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Threading;
using System.ComponentModel;
using System.Text;

public partial class InvoiceNotAvaialbleDet_LOC : System.Web.UI.Page
{
   

    
    protected void Page_Load(object sender, EventArgs e)
    {

        
            Label1.Text = "Paid to Alternate is not registered in MIS";

    }



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script type='text/JavaScript'>window.close();</script>"); 
    }
}
