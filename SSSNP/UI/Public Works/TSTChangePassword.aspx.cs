using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class TSTChangePassword : System.Web.UI.Page
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
        String UserID = "", Password = "";
        int retval = 0;


        if (BtnSave1.Text == "Submit")
        {
            UserID = Session["user_id"].ToString();
            Password = txtPwd.Text;



            General clsGeneral = new General();
            DataSet ds = clsGeneral.ValidateLoginReset(UserID, Password.ToString());
            DataView dv = ds.Tables[0].DefaultView;

            if (dv.Table.Rows.Count > 0)
            {
                retval = clsGeneral.ChangePassword(UserID, TxtNpwd.Text.ToString(), Session["uid"].ToString());
                if (retval == -1)
                {
                    lblmsg.Text = "Password Successfully Changed";
                    success.Visible = true;
                    Failure.Visible = false;
                    clear();
                }
            }
            else
            {
                success.Visible = false;
                Failure.Visible = true;
            }
        }
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
