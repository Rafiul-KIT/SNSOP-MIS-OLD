CREATE Procedure [dbo].[getGrievanceDetails]                  
(                    
@Grievance_RegistrationNo varchar(500)
)                    
as begin                    
                    
                    
   SELECT   * from   tbl_Grievance               
                    
--SELECT distinct tbl_Grievance.Grievance_RegistrationNo ,tbl_Grievance.Complainant_Name, tbl_Grievance.Complainant_EnrolmentNo, 
--tbl_Grievance.TypeOfComplaints, tbl_Grievance.Accused_Name,tbl_Grievance.Accused_EnrolmentNo ,
--tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
--tm_States.StateName   



					              
--FROM         tbl_Grievance INNER JOIN                    
--                      tm_States on tm_States.intStateid=tbl_Grievance.intStateid      
--inner join tm_Counties  on tm_Counties.intCountieid=tbl_Grievance.intCountieid      
--inner join tm_Payams on tm_Payams.intPayamid=tbl_Grievance.intPayamid      
--inner join tm_Bomas on tm_Bomas.intBomasid=tbl_Grievance.intBomasid  
                        
 where     tbl_Grievance.Grievance_RegistrationNo=@Grievance_RegistrationNo
					    
					   
                    
end




