using SSSNP_SNSOP.Core.DbHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP.UI.Public_Works
{
    public partial class RejectedWagesDeletion : System.Web.UI.Page
    {
        General Gen = new General();
        comFunctions cmf = new comFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("../../frmuserlogin.aspx"); // Boss wants us to not show this menu right now

            try
            {
                if (Session.Count == 0)
                    Response.Redirect("../../frmuserlogin.aspx");

                this.btnSearch.Attributes.Add("onclick", DisableTheButton(this.Page, this.btnSearch, "group"));

                if (!IsPostBack)
                {
                    Gen.getStates(ddlState);
                    //Gen.getIpDet(ddlIP);
                    /*if (Session["user_type"]?.ToString().Trim() == "Adm")
                    {
                        //ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"]?.ToString().Trim()).Value;
                        var ipValue = Session["User_Code"]?.ToString().Trim();
                        if (ddlIP.Items.FindByValue(ipValue) != null)
                        {
                            ddlIP.SelectedValue = ipValue;
                        }

                    }*/
                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = "Error during Page_Load: " + ex.Message;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            TxtRequestNo.Text = "";
            txtWorkcode.Text = "";
            ddlState.SelectedIndex = 0;
            ddlCounties.SelectedIndex = 0;
            ddlPayam.SelectedIndex = 0;
            ddlBoma.SelectedIndex = 0;
            //ddlIP.SelectedIndex = 0;

            grdRejectedWagesDraft.DataSource = null;
            grdRejectedWagesDraft.DataBind();
            tblreport.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGrid(0);
        }

        public string DisableTheButton(Control pge, Control btn, string vldGroup)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("if (typeof(Page_ClientValidate) == 'function') {");
            if (vldGroup.Trim() == "")
            {
                sb.Append("if (Page_ClientValidate() == false) { return false; }} ");
            }
            else
            {
                sb.Append("if (Page_ClientValidate('" + vldGroup + "') == false) { return false; }} ");
            }
            sb.Append("this.value = 'Please wait...';");
            sb.Append("this.disabled = true;");
            sb.Append(ClientScript.GetPostBackEventReference(btn, null));
            sb.Append(";");
            return sb.ToString();
        }

        private void LoadGrid(int pageIndex)
        {
            lblmessage.Text = "";
            LblResultA.Text = "";
            LblResultA0.Text = "";

            try
            {
                DataTable dt = GetRejectedWageRequestDraftsFromDB();
                if (dt.Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    ViewState["dt"] = dt;
                    grdRejectedWagesDraft.PageIndex = pageIndex;
                    grdRejectedWagesDraft.DataSource = dt;
                    grdRejectedWagesDraft.DataBind();
                }
                else
                {
                    grdRejectedWagesDraft.DataSource = null;
                    grdRejectedWagesDraft.DataBind();
                    lblmessage.Text = "No record found.";
                }
            }
            catch (Exception ex)
            {
                grdRejectedWagesDraft.DataSource = null;
                grdRejectedWagesDraft.DataBind();
                lblmessage.Text = "Error: " + ex.Message;
            }
        }

        private DataTable GetRejectedWageRequestDraftsFromDB()
        {
            DataTable dt = new DataTable();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
            SELECT DISTINCT
                    wwr.intWagePaymentReqid,
                    wwr.intWorkid,
                    wwr.ReqNo,
                    wwr.ReqFromDate,
                    wwr.ReqToDate,
                    wwr.ReqYear,
                    wwr.ReqMonth,
                    ws.StageName AS Status,
                    wwr.Created_by,
                    wwr.Created_dt,
                    wwr.Modified_by,
                    wwr.Modified_dt,
                    wwr.Component,
                    wwr.Remarks,
                    pc.WagePaymentCycle AS PaymentCycle,
                    wm.WorkCode,
                    st.StateName,
                    co.CountieName,
                    p.PayamName,
                    b.BomaName
                FROM td_WorkWageRequest wwr
                INNER JOIN tbl_WorkMaster wm ON wwr.intWorkid = wm.intWorkid
                INNER JOIN td_WageReqBenDet wrbt ON wwr.intWagePaymentReqid = wrbt.intWagePaymentReqid
                INNER JOIN tbl_BeneficiaryDet ben ON wrbt.intBenid = ben.intBenid
                INNER JOIN tm_States st ON st.intStateid = ben.intStateid
                INNER JOIN tm_Counties co ON ben.intCountieid = co.intCountieid
                INNER JOIN tm_Payams p ON ben.intPayamid = p.intPayamid
                INNER JOIN tm_Bomas b ON ben.intBomasid = b.intBomasid
                LEFT JOIN tm_WagePaymentCycle pc ON wwr.PaymentCycle = pc.intPaymentCycleID
                LEFT JOIN tm_WageStages ws ON wwr.Status = ws.Stageid
                WHERE 
                    wwr.Status IN (100, 101, 102, 105)
                    AND (@ReqNo IS NULL OR wwr.ReqNo = @ReqNo)
                    AND (@workCode IS NULL OR wm.WorkCode = @workCode)
                    AND (@intStateid IS NULL OR ben.intStateid = @intStateid)
                    AND (@intCountieid IS NULL OR ben.intCountieid = @intCountieid)
                    AND (@intPayamid IS NULL OR ben.intPayamid = @intPayamid)
                    AND (@intBomasid IS NULL OR ben.intBomasid = @intBomasid)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@workCode", string.IsNullOrWhiteSpace(txtWorkcode.Text.Trim()) ? (object)DBNull.Value : txtWorkcode.Text.Trim());
                    cmd.Parameters.AddWithValue("@ReqNo", string.IsNullOrWhiteSpace(TxtRequestNo.Text.Trim()) ? (object)DBNull.Value : TxtRequestNo.Text.Trim());

                    cmd.Parameters.AddWithValue("@intStateid", GetNullableInt(ddlState.SelectedValue));
                    cmd.Parameters.AddWithValue("@intCountieid", GetNullableInt(ddlCounties.SelectedValue));
                    cmd.Parameters.AddWithValue("@intPayamid", GetNullableInt(ddlPayam.SelectedValue));
                    cmd.Parameters.AddWithValue("@intBomasid", GetNullableInt(ddlBoma.SelectedValue));
                    //cmd.Parameters.AddWithValue("@IPid", GetNullableInt(ddlIP.SelectedValue));

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private object GetNullableInt(string value)
        {
            return int.TryParse(value, out int result) ? (object)result : DBNull.Value;
        }



        protected void grdRejectedWagesDraft_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRejectedWagesDraft.PageIndex = e.NewPageIndex;
            LoadGrid(e.NewPageIndex);
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            lblDeleteWarning.Text = "";
            lblDeleteWarning.Text = "";

            int workReqId;
            if (!int.TryParse(hdnDeleteWorkReqId.Value, out workReqId))
            {
                lblDeleteWarning.Text = "Invalid request ID.";
                return;
            }

            string comment = txtDeleteComment.Text.Trim();
            if (string.IsNullOrWhiteSpace(comment))
            {
                lblDeleteWarning.Text = "Comment is required.";
                return;
            }


            try
            {
                DeleteRejectedWageRequest(workReqId, comment);

                lblmessage.Text = "Record deleted successfully.";
                lblmessage.ForeColor = System.Drawing.Color.Green;

                var emailService = new SSSNP.Network.EmailService();
                string subject = "Wage Request Deleted";
                string body = $"Wage request ID {workReqId} was deleted by user {Session["uid"]} for reason: {comment}";
                emailService.SendEmailToIPs(subject, body);

                txtDeleteComment.Text = "";
                hdnDeleteWorkReqId.Value = "";
                lblDeleteWarning.Text = "";

                LoadGrid(0);

                ScriptManager.RegisterStartupScript(this, GetType(), "HidePopup", "hideDeletePopup();", true);
            }
            catch (Exception ex)
            {
                lblDeleteWarning.Text = "Something went wrong during deletion. Please try again or contact admin.";
                lblDeleteWarning.ForeColor = System.Drawing.Color.Red;

                ScriptManager.RegisterStartupScript(this, GetType(), "ShowPopup", "showDeletePopup();", true);
                Debug.WriteLine("Error: " + ex);
            }

        }

        private void DeleteRejectedWageRequest(int workReqId, string comment)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            IDbOperation db = new SqlDbOperation(connStr); 
            var service = new Service.RejectedWageRequestDeletionService(db, connStr);

            string deletedBy = Session["uid"]?.ToString();

            service.DeleteRejectedWageRequest(workReqId, comment, deletedBy);
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCounties.Items.Clear();
            ddlPayam.Items.Clear();
            if (ddlState.SelectedIndex != 0)
            {
                General Gen = new General();
                Gen.getCounties(ddlCounties, ddlState.SelectedValue);
            }
            else
            {
                ddlCounties.Items.Insert(0, "--Select--");
                ddlPayam.Items.Insert(0, "--Select--");
            }
        }

        protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPayam.Items.Clear();
            if (ddlCounties.SelectedIndex != 0)
            {
                General Gen = new General();
                Gen.getPayams(ddlPayam, ddlCounties.SelectedValue);
            }
            else
            {
                ddlPayam.Items.Insert(0, "--Select--");
            }
        }

        protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBoma.Items.Clear();
            if (ddlPayam.SelectedIndex != 0)
            {
                General Gen = new General();
                Gen.getBomas(ddlBoma, ddlPayam.SelectedValue);
            }
            else
            {
                ddlBoma.Items.Insert(0, "--Select--");
            }
        }


        protected void grdRejectedWagesDraft_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Optional: Highlight rows, format dates, etc.
        }

        protected void grdRejectedWagesDraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: Handle row selection logic
        }

        protected void grdRejectedWagesDraft_PreRender(object sender, EventArgs e)
        {
            // Optional: Use this to improve accessibility or force header rendering on empty GridView
            grdRejectedWagesDraft.UseAccessibleHeader = true;
            if (grdRejectedWagesDraft.HeaderRow != null)
            {
                grdRejectedWagesDraft.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


    }
}