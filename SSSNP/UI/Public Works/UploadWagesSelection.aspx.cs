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

public partial class UploadWagesSelection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {
            lblresult0.Visible = false;
            trstat.Visible = false;
        }
        
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

            OleDbCommand cmd = new OleDbCommand("Select Sno,RequestNo,EnrollmentNumber,WorkCode,SSPExchangeRate from [WagesBenSelection$]", excelConnection);

            excelConnection.Open();
            OleDbDataAdapter Oledbadp = new OleDbDataAdapter();
            Oledbadp.SelectCommand = cmd;

            Oledbadp.Fill(dsImp);
            if (dsImp != null)
            {
                if (dsImp.Tables.Count > 0)
                {
                    string s = CheckErrors(dsImp.Tables[0]);
                    if (s == "")
                    {
                        excelUpload(dsImp.Tables[0]);

                    }
                    else
                    {
                        lblresult0.Text = s;
                        lblresult0.ForeColor = System.Drawing.Color.Red;
                        lblresult0.Visible = true;
                    }
                }
            }
        }
        catch { }
    }
    private string  CheckErrors(DataTable dt)
    {
        comFunctions cmf = new comFunctions();
        int i = 1;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr[4].ToString() == "" && dr[3].ToString() == "" && dr[2].ToString() == "" && dr[1].ToString() == "" && dr[0].ToString() == "")
            { break; }
            else
            {
                if (dr[4] != null)
                {

                    if (dr[4].ToString().Trim() != "")
                    { }
                    else
                    { return "Please Check SSPExchangeRate in Row " + i.ToString(); }
                }else
                { return "Please Check SSPExchangeRate in Row " + i.ToString(); }
                
                if (dr[3] != null)
                {

                    if (dr[3].ToString().Trim() != "")
                    { }
                    else
                    { return "Please Check WorkCode in Row " + i.ToString(); }
                }
                else
                { return "Please Check WorkCode in Row " + i.ToString(); }

                if (dr[0] != null)
                {
                    if (dr[0].ToString().Trim() != "")
                    { }
                    else
                    { return "Please Check Sno in Row " + i.ToString(); }
                   

                }
                else
                { return "Please Check Sno in Row " + i.ToString(); }

                if (dr[1] != null)
                {
                    if (dr[1].ToString().Trim() != "")
                    { }
                    else
                    { return "Please Check RequestNo in Row " + i.ToString(); }
                   

                }
                else
                { return "Please Check RequestNo in Row " + i.ToString(); }

                if (dr[2] != null)
                {
                    if (dr[2].ToString().Trim() != "")
                    { }
                    else
                    { return "Please Check EnrollmentNumber in Row " + i.ToString(); }


                }
                else
                { return "Please Check EnrollmentNumber in Row " + i.ToString(); }




               
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
        if (Request.QueryString[0].ToString().Trim() == dt.Rows[0][1].ToString())
        {
            int resultwage = Gen.insertWageRequestPaymentImport(dt.Rows[0][1].ToString(), dt.Rows[0][4].ToString(), Session["uid"].ToString().Trim());




            foreach (DataRow dr in dt.Rows)
            {
                if (dr[4].ToString() == "" && dr[3].ToString() == "" && dr[2].ToString() == "" && dr[1].ToString() == "" && dr[0].ToString() == "")
                {
                    break;
                }
                else
                {
                    DataSet ds = new DataSet();
                    ds = Gen.GetRequestIstherewithBenficiaryandWorkCodePA(dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            hdfReqid.Value = ds.Tables[0].Rows[0]["intWagePaymentReqid"].ToString();
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                hdfWorkid.Value = ds.Tables[1].Rows[0]["intWorkid"].ToString();
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    hdfBenid.Value = ds.Tables[2].Rows[0]["intBenid"].ToString();
                                    if (ds.Tables[3].Rows.Count > 0)
                                    {

                                        DataRow row1 = dtFail.NewRow();
                                        row1[0] = dr[0];
                                        row1[1] = dr[1];
                                        row1[2] = dr[2];
                                        row1[3] = dr[3];
                                        row1[4] = dr[4];
                                        row1["Remarks"] = "Already Uploaded in our MIS";
                                        dtFail.Rows.InsertAt(row1, cf);
                                        cf++;

                                    }
                                    else
                                    {


                                        //  resultwage

                                        if (dt.Rows[0][3].ToString() == dr[3].ToString() && dt.Rows[0][1].ToString() == dr[1].ToString() && dt.Rows[0][1].ToString() == Request.QueryString[0].ToString())
                                        {

                                            int s1 = Gen.InsUpdBeneficiaryWagesRequestPayingAgentImport(hdfReqid.Value, hdfWorkid.Value, hdfBenid.Value, dr[4].ToString(), Session["uid"].ToString(), resultwage.ToString());

                                            // s = Gen.BenefeciaryAttendanceNew(dr[0].ToString(), dr[1].ToString(), dr[3].ToString(), dr[2].ToString(), Session["uid"].ToString());
                                            DataRow row = dtSuccess.NewRow();
                                            row[0] = dr[0];
                                            row[1] = dr[1];
                                            row[2] = dr[2];
                                            row[3] = dr[3];
                                            row[4] = dr[4];
                                            row["Remarks"] = "Successfully Imported";
                                            dtSuccess.Rows.InsertAt(row, cs);
                                            cs++;
                                        }
                                        else
                                        {
                                            DataRow row1 = dtFail.NewRow();
                                            row1[0] = dr[0];
                                            row1[1] = dr[1];
                                            row1[2] = dr[2];
                                            row1[3] = dr[3];
                                            row1[4] = dr[4];
                                            row1["Remarks"] = "Not in this requst no data as per our MIS";
                                            dtFail.Rows.InsertAt(row1, cf);
                                            cf++;
                                        }
                                    }
                                }
                                else
                                {
                                    DataRow row1 = dtFail.NewRow();
                                    row1[0] = dr[0];
                                    row1[1] = dr[1];
                                    row1[2] = dr[2];
                                    row1[3] = dr[3];
                                    row1[4] = dr[4];
                                    row1["Remarks"] = "Enrollement Number is not in our MIS";
                                    dtFail.Rows.InsertAt(row1, cf);
                                    cf++;
                                }
                            }
                            else
                            {
                                DataRow row1 = dtFail.NewRow();
                                row1[0] = dr[0];
                                row1[1] = dr[1];
                                row1[2] = dr[2];
                                row1[3] = dr[3];
                                row1[4] = dr[4];
                                row1["Remarks"] = "Work Code is not in our MIS";
                                dtFail.Rows.InsertAt(row1, cf);
                                cf++;
                            }
                        }



                        else
                        {
                            DataRow row1 = dtFail.NewRow();
                            row1[0] = dr[0];
                            row1[1] = dr[1];
                            row1[2] = dr[2];
                            row1[3] = dr[3];
                            row1[4] = dr[4];
                            row1["Remarks"] = "Request No is not in our MIS";
                            dtFail.Rows.InsertAt(row1, cf);
                            cf++;
                        }
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




            if (s == 999 || s == 1)
            {
                lblresult0.Text = "Wages Selection Successfully Uploaded ";
                lblresult0.Visible = true;
                lblresult0.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                lblresult0.Text = "Wages Selection Not Successfully Uploaded ";
                lblresult0.Visible = true;
                lblresult0.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lblresult0.Text = "Wrong Wage Request No Given";
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
        string FileName = "Wage Selection Failure Details" + DateTime.Now + ".xls";
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
        string FileName = "Wage Selection Success Details" + DateTime.Now + ".xls";
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