using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

public partial class frmChangePassword : System.Web.UI.Page
{
    General Gen = new General();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <=0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        else
        {
            
            lbluserid.Text = Session["username"].ToString();
            chkpwd();
        }
       
    }

    void chkpwd()
    {
        int i = Gen.chkpwd(Session["user_id"].ToString());
        if (i == 1000)
        {
            warning.Visible = true;
            lbluserid0.Text = "Please remember to change your password the first time you access..";
        }
        else if (i == 99)
        {
            lbluserid0.Text = "Please change your password periodically.";
            warning.Visible = true;
            
        }
        else
        {
            warning.Visible = false;
            
        }
        
    }

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //String UserID = "", Password = "", NewPassword = "";
        //int retval = 0;


        //if (BtnSave1.Text == "Submit")
        //{
        //    General clsGeneral = new General();

        //    UserID = Session["user_id"].ToString();
        //    if (!String.IsNullOrEmpty(txtPwd.Text))
        //        Password = clsGeneral.CalculateMD5Hash(txtPwd.Text);
        //    //Password = txtPwd.Text;
            
        //    DataSet ds = clsGeneral.ValidateLoginReset(UserID, Password.ToString());
        //    DataView dv = ds.Tables[0].DefaultView;

        //    if (dv.Table.Rows.Count > 0)
        //    {
        //        if (!String.IsNullOrEmpty(TxtNpwd.Text))
        //            NewPassword = clsGeneral.CalculateMD5Hash(TxtNpwd.Text);
        //        retval = clsGeneral.ChangePassword(UserID, NewPassword, Session["uid"].ToString());
        //        if (retval == -1)
        //        {
        //            lblmsg.Text = "Password Successfully Changed";
        //            success.Visible = true;
        //            Failure.Visible = false;
        //            clear();
        //        }
        //    }
        //    else
        //    {
        //        success.Visible = false;
        //        Failure.Visible = true;
        //    }
        //}

    }
    void clear()
    {
        BtnSave1.Text = "Submit";
        TxtNpwd.Text = "";
        txtPwd.Text = "";
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmChangePassword.aspx");        
    }
}
