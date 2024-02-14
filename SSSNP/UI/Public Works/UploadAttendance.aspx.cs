using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadAttendance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {
            this.btnSend.Attributes.Add("onclick", DisableTheButton(this.Page, this.btnSend, "group"));
            lblresult0.Visible = false;
            trstat.Visible = false;
        }

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
        sb.Append(pge.Page.GetPostBackEventReference(btn));
        //sb.Append(pge.Page.GetPostBackEventReference(btn));
        sb.Append(";");
        return sb.ToString();
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            string path = "";
            string saveFolder = @"C:\\temp\\uploads";

            string newPath = "";

            //Create a new subfolder under the current active folder
            newPath = System.IO.Path.Combine(saveFolder, DateTime.Now.ToString("ddMMyyyyhhmmsstt").Trim());

            // Create the subfolder
            if (!Directory.Exists(newPath))
                System.IO.Directory.CreateDirectory(newPath);

            path = newPath + "\\" + fupLetter.FileName;

            fupLetter.SaveAs(path);
            string excelConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", path);
            //Create Connection to Excel work book
            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
            DataSet dsImp = new DataSet();
            //OleDbCommand cmd = new OleDbCommand("Select [Training Center],[Project Name],[Trainee Name],[Father / Husband Name],[Date of Birth (MM/DD/YYYY)],[Category],[Gender], [Education Qualification],Address,[Present State],[Present District],[Present Block],[Present Gram Panchayat],[Present PIN Code],[Mobile No],[Alternative Mobile No],[Email],[Religion],[Adhaar No] , [Beneficiary S No] as sno,Trade,Education,Languages from [TraineeDetails$]", excelConnection);

            OleDbCommand cmd = new OleDbCommand("Select [Work Code],[Enrollment No],[Attendance Date],[Status] from [Benficiary Attedance$]", excelConnection);

            excelConnection.Open();
            OleDbDataAdapter Oledbadp = new OleDbDataAdapter();
            Oledbadp.SelectCommand = cmd;

            Oledbadp.Fill(dsImp);
            if (dsImp != null)
            {
                if (dsImp.Tables.Count > 0)
                {
                   // string s = CheckErrors(dsImp.Tables[0]);
                    //if (s == "")
                    //{
                        excelUpload(dsImp.Tables[0]);

                    //}
                    //else
                    //{
                    //    lblresult0.Text = s;
                    //    lblresult0.ForeColor = System.Drawing.Color.Red;
                    //    lblresult0.Visible = true;
                    //}
                }
            }
        }
        catch { }
    }
    private string  CheckErrors(DataTable dt)
    {
      //  comFunctions cmf = new comFunctions();
        int i = 1;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr[3].ToString() == "" && dr[2].ToString() == "" && dr[1].ToString() == "" && dr[0].ToString() == "")
            { break; }
            else
            {
                if (dr[3] != null)
                {

                    if (dr[3].ToString().Trim() != "")
                    { }
                    else
                    { return "Please Check Status in Row " + i.ToString(); }
                }
                else
                { return "Please Check Status in Row " + i.ToString(); }

                if (dr[0] != null)
                {
                    if (dr[0].ToString().Trim() != "")
                    { }
                    else
                    { return "Please Check Work Code in Row " + i.ToString(); }
                   

                }
                else
                { return "Please Check Work Code in Row " + i.ToString(); }

                if (dr[1] != null)
                {
                    if (dr[1].ToString().Trim() != "")
                    { }
                    else
                    { return "Please Check Enrollment No in Row " + i.ToString(); }
                   

                }
                else
                { return "Please Check Enrollment No in Row " + i.ToString(); }

                if (dr[2] != null)
                {
                    DateTime x;
                    try { x = Convert.ToDateTime((dr[2].ToString().Trim())); }
                    catch { return "Please Check Attendance Date in Row " + i.ToString(); }

                    if (x > Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-mm-dd")))
                    {
                        return "Attendance Date in Row " + i.ToString() + " Should be Less Than Current Date.";
                    }
                }
                else
                { return "Please Check Attendance Date in Row " + i.ToString(); }




               
            }
            i = i + 1;
        }
        return "";

    }

    void excelUpload(DataTable dt)
    {
        dt.Columns.Add("Remarks", typeof(string));
        General Gen = new General();
        int s = 0;
        DataTable dtSuccess = dt.Clone();
        dtSuccess.Clear();
        DataTable dtFail = dt.Clone();
        dtFail.Clear();
        int cs = 0;
        int cf = 0;
        foreach (DataRow dr in dt.Rows)
        {
                   if (dr[3].ToString() == "" && dr[2].ToString() == "" && dr[1].ToString() == "" && dr[0].ToString() == "")
                    {
                        DataRow row1 = dtFail.NewRow();
                        row1[0] = dr[0];
                        row1[1] = dr[1];
                        row1[2] = dr[2];
                        row1[3] = dr[3];
                        row1["Remarks"] = "Manditory fields should be fill";
                        dtFail.Rows.InsertAt(row1, cf);
                        cf++;
                    }
                 
                    else
                    {


                       
                        try {
                            
                           

                            if (Convert.ToDateTime((dr[2].ToString().Trim())) > Convert.ToDateTime(System.DateTime.Now))
                            {

                                DataRow row1 = dtFail.NewRow();
                                row1[0] = dr[0];
                                row1[1] = dr[1];
                                row1[2] = dr[2];
                                row1[3] = dr[3];
                                row1["Remarks"] = "Attendance Date Should be Less Than Current Date.";
                                dtFail.Rows.InsertAt(row1, cf);
                                cf++;

                            }
                            else
                            {

                                DataSet ds = new DataSet();
                                ds = Gen.GetWorkCodeIsCompleted(dr[0].ToString());
                                if (ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {


                                        s = Gen.BenefeciaryAttendanceNewUpload(dr[0].ToString(), dr[1].ToString(), dr[3].ToString(), dr[2].ToString(), Session["uid"].ToString());

                                        if (s == 1)
                                        {

                                            DataRow row = dtSuccess.NewRow();
                                            row[0] = dr[0];
                                            row[1] = dr[1];
                                            row[2] = dr[2];
                                            row[3] = dr[3];
                                            row["Remarks"] = "Successfully Imported";
                                            dtSuccess.Rows.InsertAt(row, cs);
                                            cs++;
                                        }
                                        else if (s == 977)
                                        {
                                            DataRow row1 = dtFail.NewRow();
                                            row1[0] = dr[0];
                                            row1[1] = dr[1];
                                            row1[2] = dr[2];
                                            row1[3] = dr[3];
                                            row1["Remarks"] = "Beneficiary is not under this IP";
                                            dtFail.Rows.InsertAt(row1, cf);
                                            cf++;
                                        }
                                        else if (s == 966)
                                        {
                                            DataRow row1 = dtFail.NewRow();
                                            row1[0] = dr[0];
                                            row1[1] = dr[1];
                                            row1[2] = dr[2];
                                            row1[3] = dr[3];
                                            row1["Remarks"] = "Work is not under this IP";
                                            dtFail.Rows.InsertAt(row1, cf);
                                            cf++;
                                        }
                                        else if (s == 999)
                                        {
                                            DataRow row1 = dtFail.NewRow();
                                            row1[0] = dr[0];
                                            row1[1] = dr[1];
                                            row1[2] = dr[2];
                                            row1[3] = dr[3];
                                            row1["Remarks"] = "It is already uploaded";
                                            dtFail.Rows.InsertAt(row1, cf);
                                            cf++;
                                        }
                                        else if (s == 988)
                                        {
                                            DataRow row1 = dtFail.NewRow();
                                            row1[0] = dr[0];
                                            row1[1] = dr[1];
                                            row1[2] = dr[2];
                                            row1[3] = dr[3];
                                            row1["Remarks"] = "It is already applied wage request";
                                            dtFail.Rows.InsertAt(row1, cf);
                                            cf++;
                                        }
                                        //DataRow row = dtSuccess.NewRow();
                                        //row[0] = dr[0];
                                        //row[1] = dr[1];
                                        //row[2] = dr[2];
                                        //row[3] = dr[3];
                                        //row["Remarks"] = "Successfully Imported";
                                        //dtSuccess.Rows.InsertAt(row, cs);
                                        //cs++;
                                    }



                                    else
                                    {
                                        DataRow row1 = dtFail.NewRow();
                                        row1[0] = dr[0];
                                        row1[1] = dr[1];
                                        row1[2] = dr[2];
                                        row1[3] = dr[3];
                                        row1["Remarks"] = "Completed Work";
                                        dtFail.Rows.InsertAt(row1, cf);
                                        cf++;
                                    }
                                }
                            }
                        
                        
                        
                        }
                        catch
                        {


                            DataRow row1 = dtFail.NewRow();
                            row1[0] = dr[0];
                            row1[1] = dr[1];
                            row1[2] = dr[2];
                            row1[3] = dr[3];
                            row1["Remarks"] = "Please Check Attendance Date";
                            dtFail.Rows.InsertAt(row1, cf);
                            cf++;
                           
                        }

                        
                    }

            
        }
        if (dtSuccess.Rows.Count > 0 || dtFail.Rows.Count > 0)
        {
            Label94.Text = Label94.Text + " : " + dtSuccess.Rows.Count;
            Label93.Text = Label93.Text + " : " + dtFail.Rows.Count;
            GridView2.DataSource = dtSuccess;
            GridView2.DataBind();
            GridView1.DataSource = dtFail;
            GridView1.DataBind();
            trstat.Visible = true;
        }
        else
        { trstat.Visible = false; }
       



        if (s == 999||s==1)
        {
            lblresult0.Text = "Attendance Successfully Uploaded ";
            lblresult0.Visible = true;
            lblresult0.ForeColor = System.Drawing.Color.Green ;

        }
        else
        {
            lblresult0 .Text = "Attendance Not Successfully Uploaded ";
            lblresult0.Visible = true;
            lblresult0.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnSend1_Click(object sender, EventArgs e)
    {
        ExportGridToExcelFail();

    }
    protected void btnSend0_Click(object sender, EventArgs e)
    {
        ExportGridToExcelSuccess();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    private void ExportGridToExcelFail()
    {
        Response.Clear();
        Response.Buffer = true;
        string FileName = "Attendance Failure Details" + DateTime.Now + ".xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridView1.AllowPaging = false;
          //  this.fillgrid();
            //grdDetails.Columns[0].Visible = false;
            GridView1.HeaderRow.BackColor = System.Drawing.Color.White;
            //grdDetails.BorderColor = Color.Black;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;

            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    cell.Attributes.Add("class", "sborder");
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .sborder {border : 1px Solid black; }</style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }

    }
    private void ExportGridToExcelSuccess()
    {
        Response.Clear();
        Response.Buffer = true;
        string FileName = "Attendance Success Details" + DateTime.Now + ".xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridView2.AllowPaging = false;
          //  this.fillgrid();
            //grdDetails.Columns[0].Visible = false;
            GridView2.HeaderRow.BackColor = System.Drawing.Color.White;
            //grdDetails.BorderColor = Color.Black;
            foreach (TableCell cell in GridView2.HeaderRow.Cells)
            {
                cell.BackColor = GridView2.HeaderStyle.BackColor;

            }
            foreach (GridViewRow row in GridView2.Rows)
            {
                row.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    cell.Attributes.Add("class", "sborder");
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView2.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView2.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .sborder {border : 1px Solid black; }</style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }

    }


}