using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

public partial class ImportFromServiceUpdatePhotoAlternate1DetLatest : System.Web.UI.Page
{
    General Gen = new General();

    DataTable mydt = new DataTable();
    static DataTable dtMyTable;
    static DataTable dtMyTableCertificate;
    DataTable mydtp = new DataTable();
    static DataTable dtMyTablep;
    static DataTable dtMyTableCertificatep;
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Server.ScriptTimeout = 800;

        if (!IsPostBack)
        {
           
            string result = InsertBenAlternateDetails();
            Response.Write(result);
          
        }

    }

  

    /// <summary>
    /// Insert Data Using API
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public string InsertBenAlternateDetails()
    {
        string mainresult = "";
      
             try{
                                 //Update Alternate Photo
        DataSet ds_check=new DataSet();
        ds_check=Gen.getAlterateonetoupdatephoto();
        if(ds_check.Tables[1].Rows.Count>0)
        {
int cna=ds_check.Tables[1].Rows.Count;
for (int i = 0; i < cna; i++)
{
    string Directory_Path = Server.MapPath("~/Alternatetwo/" + Convert.ToInt32(ds_check.Tables[1].Rows[i]["intBenTempid"].ToString().Trim()));
    string Path1 = Server.MapPath("~/Alternatetwo/" + Convert.ToInt32(ds_check.Tables[1].Rows[i]["intBenTempid"].ToString().Trim()));
    //string Path2 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/FirstAlternate");
    //string Path3 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/SecondAlternate");
    if (!Directory.Exists(Directory_Path))
    {
        Directory.CreateDirectory(Directory_Path);

        Directory.CreateDirectory(Path1);
        //Directory.CreateDirectory(Path2);
        //Directory.CreateDirectory(Path3);
    }
    
        try
        {
            //string Alt_File_Url = "http:/54.68.125.132:8181/snsdp/public/attachments/" + Convert.ToString(ds_check.Tables[1].Rows[i]["SecondAlternatepayeePhotoName"].ToString());
            string Alt_File_Url = System.Configuration.ConfigurationManager.AppSettings["GetAttachments"] + Convert.ToString(ds_check.Tables[1].Rows[i]["SecondAlternatepayeePhotoName"].ToString()); // API_V_1.2
            byte[] Alt_data;
            using (WebClient Alt_client = new WebClient())
            {
                Alt_data = Alt_client.DownloadData(Alt_File_Url);
            }
            string household_photo_path = "~/Alternatetwo/" + Convert.ToInt32(ds_check.Tables[0].Rows[i]["intBenTempid"].ToString().Trim()) + "/" + Convert.ToString(ds_check.Tables[1].Rows[i]["SecondAlternatepayeePhotoName"].ToString());
            string alternate_photo_path = Path1 + "/" + Convert.ToString(ds_check.Tables[1].Rows[i]["SecondAlternatepayeePhotoName"].ToString());
            if (File.Exists(household_photo_path))
            {
                File.Delete(household_photo_path);
            }
            File.WriteAllBytes(alternate_photo_path, Alt_data);
            int p = Gen.UploadHouseHoldPhoto(Convert.ToInt32(ds_check.Tables[1].Rows[i]["intBenTempid"].ToString().Trim()), alternate_photo_path, "SecondAlternatePayeePhotoPath", ds_check.Tables[1].Rows[i]["SecondAlternatepayeePhotoName"].ToString());
        }
        catch (Exception ex)
        {
            if (ex.ToString() == "The remote server returned an error: (404) Not Found")
            {
                Gen.InsertAPIFailure_Status(ds_check.Tables[1].Rows[i]["Secondalternate_number"].ToString(), "File is not in API URL (Alternate two Payconnect Team)", System.DateTime.Now.ToString());



            }
            else
            {
                Gen.InsertAPIFailure_Status(ds_check.Tables[1].Rows[i]["Secondalternate_number"].ToString(), ex.ToString(), System.DateTime.Now.ToString());


            }



        }

    
}
        
        
        
        
        
        }




                            
                            if(ds_check.Tables[0].Rows.Count>0)
                           {

                               int cna = ds_check.Tables[0].Rows.Count;
                               for (int i = 0; i < cna; i++)
                               {
                                   string Directory_Path = Server.MapPath("~/Alternateone/" + Convert.ToInt32(ds_check.Tables[0].Rows[i]["intBenTempid"].ToString().Trim()));
                                   string Path1 = Server.MapPath("~/Alternateone/" + Convert.ToInt32(ds_check.Tables[0].Rows[i]["intBenTempid"].ToString().Trim()));
                                   //string Path2 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/FirstAlternate");
                                   //string Path3 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/SecondAlternate");
                                   if (!Directory.Exists(Directory_Path))
                                   {
                                       Directory.CreateDirectory(Directory_Path);

                                       Directory.CreateDirectory(Path1);
                                       //Directory.CreateDirectory(Path2);
                                       //Directory.CreateDirectory(Path3);
                                   }

                                   try
                                   {
                                       //string Alt_File_Url = "http:/54.68.125.132:8181/snsdp/public/attachments/" + Convert.ToString(ds_check.Tables[0].Rows[i]["FirstAlternatePayeePhotoName"].ToString());
                                       string Alt_File_Url = System.Configuration.ConfigurationManager.AppSettings["GetAttachments"] + Convert.ToString(ds_check.Tables[0].Rows[i]["FirstAlternatePayeePhotoName"].ToString()); // API_V_1.2
                                       byte[] Alt_data;
                                       using (WebClient Alt_client = new WebClient())
                                       {
                                           Alt_data = Alt_client.DownloadData(Alt_File_Url);
                                       }
                                       string household_photo_path = "~/Alternateone/" + Convert.ToInt32(ds_check.Tables[0].Rows[i]["intBenTempid"].ToString().Trim()) + "/" + Convert.ToString(ds_check.Tables[0].Rows[i]["FirstAlternatePayeePhotoName"].ToString());
                                       string alternate_photo_path = Path1 + "/" + Convert.ToString(ds_check.Tables[0].Rows[i]["FirstAlternatePayeePhotoName"].ToString());
                                       if (File.Exists(household_photo_path))
                                       {
                                           File.Delete(household_photo_path);
                                       }
                                       File.WriteAllBytes(alternate_photo_path, Alt_data);
                                       int p = Gen.UploadHouseHoldPhoto(Convert.ToInt32(ds_check.Tables[0].Rows[i]["intBenTempid"].ToString().Trim()), alternate_photo_path, "FirstAlternatePayeePhotoPath", ds_check.Tables[0].Rows[i]["FirstAlternatePayeePhotoName"].ToString());
                                   }
                                   catch (Exception ex)
                                   {
                                       if (ex.ToString() == "The remote server returned an error: (404) Not Found")
                                       {
                                           Gen.InsertAPIFailure_Status(ds_check.Tables[0].Rows[i]["alternate_number"].ToString(), "File is not in API URL (Alternate one Payconnect Team)", System.DateTime.Now.ToString());



                                       }
                                       else
                                       {
                                           Gen.InsertAPIFailure_Status(ds_check.Tables[0].Rows[i]["alternate_number"].ToString(), ex.ToString(), System.DateTime.Now.ToString());


                                       }



                                   }


                               }
        

                              //   check = 1;
                             }

                             //AddDataToTableCeertificate((DataTable)Session["ErrorDataA"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.alternate_number,
                             //           bad.alternate_name, bad.alternate_name, bad.alternate_name, bad.age, bad.id_number, bad.phone_number, bad.gender,
                             //          bad.photo_url, "", bad.created_at, bad.updated_at, "Success", "0", System.DateTime.Now.ToString(), ds_check.Tables[0].Rows[0]["EnrollmentNo"].ToString().Trim());


                            
                        
                        

                      
                  
                    // success message


                    //DataSet dsa = new DataSet();

                    //dsa.Tables.Add((DataTable)Session["ErrorDataA"]); 
                    


                    //// JSON Return Process

                    //if (dsa.Tables[0].Rows.Count > 0)
                    //{
                    //    mainresult = mainresult + "{";
                    //    mainresult = "{ \"error\":\"false\",\"errorCode\":" + "\"" + "0" + "\",\"data\":[";
                    //    foreach (DataTable table in dsa.Tables)
                    //    {
                    //        foreach (DataRow dr in table.Rows)
                    //        {
                    //            mainresult = mainresult + "{\"id\":\"" + dr["id"].ToString() + "\",";

                    //            //mainresult = mainresult + "\"supervisor_id\":\"" + dr["supervisor_id"].ToString() + "\",";
                              
                    //            mainresult = mainresult + "\"household_number\":\"" + dr["household_number"].ToString() + "\",";
                    //            mainresult = mainresult + "\"alternate_number\":\"" + dr["alternate_number"].ToString() + "\",";

                    //            //mainresult = mainresult + "\"alternate_name\":\"" + dr["first_name"].ToString() + "\",";
                            
                    //            //mainresult = mainresult + "\"age\":\"" + dr["age"].ToString() + "\",";
                               
                    //            //mainresult = mainresult + "\"id_number\":\"" + dr["id_number"].ToString() + "\",";
                    //            //mainresult = mainresult + "\"phone_number\":\"" + dr["phone_number"].ToString() + "\",";
                    //            //mainresult = mainresult + "\"gender\":\"" + dr["gender"].ToString() + "\",";
                               
                    //            //mainresult = mainresult + "\"photo_url\":\"" + dr["photo_url"].ToString() + "\",";
                    //            //mainresult = mainresult + "\"id_url\":\"" + dr["id_url"].ToString() + "\",";
                    //            //mainresult = mainresult + "\"created_at\":\"" + dr["created_at"].ToString() + "\",";
                    //            //mainresult = mainresult + "\"updated_at\":\"" + dr["updated_at"].ToString() + "\",";
                    //            mainresult = mainresult + "\"Status\":\"" + dr["Status"].ToString() + "\",";
                    //            mainresult = mainresult + "\"Reason\":\"" + dr["Reason"].ToString() + "\",";
                    //            //mainresult = mainresult + "\"ReturnDate\":\"" + dr["ReturnDate"].ToString() + "\",";

                    //            mainresult = mainresult + "\"benid\":\"" + dr["benid"].ToString() + "\"},";


                    //        }
                    //        mainresult = mainresult.Substring(0, mainresult.Length - 1) + "]}";
                    //    }
                    //}
                    //else
                    //{
                    //    mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found\"}";
                    //}


                    mainresult = "{ \"error\":\"true\",\"Done\":\"1\",\"data\": \"data not found\"}";


                   

            
        }
        catch (Exception ex)
        {
            mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found\"}";
           // Test = false;
          //  ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('url does not exist')", true);
           // return;
        }
      

        return mainresult;
    }

   
  
}
