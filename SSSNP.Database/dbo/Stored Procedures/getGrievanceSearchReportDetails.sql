CREATE Procedure [dbo].[getGrievanceSearchReportDetails]               
(                    
@intBomasid  varchar(10),
	@FromDate  date ,
	@ToDate  date
)                    
as begin                    
                    
  if(@FromDate is null and @ToDate is null)
 begin
SELECT  tbl_Grievance.Grievance_RegistrationNo ,tbl_Grievance.Complainant_Name, tbl_Grievance.Complainant_EnrolmentNo, 
tbl_Grievance.TypeOfComplaints, tbl_Grievance.Accused_Name,tbl_Grievance.Accused_EnrolmentNo ,
tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
tm_States.StateName   



					              
FROM         tbl_Grievance INNER JOIN                    
                      tm_States on tm_States.intStateid=tbl_Grievance.intStateid      
inner join tm_Counties  on tm_Counties.intCountieid=tbl_Grievance.intCountieid      
inner join tm_Payams on tm_Payams.intPayamid=tbl_Grievance.intPayamid      
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_Grievance.intBomasid 


where tbl_Grievance.intBomasid like @intBomasid
 end                                   
 else
 begin                    
                    
                    
SELECT  tbl_Grievance.Grievance_RegistrationNo ,tbl_Grievance.Complainant_Name, tbl_Grievance.Complainant_EnrolmentNo, 
tbl_Grievance.TypeOfComplaints, tbl_Grievance.Accused_Name,tbl_Grievance.Accused_EnrolmentNo ,
tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
tm_States.StateName   



					              
FROM         tbl_Grievance INNER JOIN                    
                      tm_States on tm_States.intStateid=tbl_Grievance.intStateid      
inner join tm_Counties  on tm_Counties.intCountieid=tbl_Grievance.intCountieid      
inner join tm_Payams on tm_Payams.intPayamid=tbl_Grievance.intPayamid      
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_Grievance.intBomasid  
                                          
                      where convert(date,tbl_Grievance.ComplaintRegisteredDate) between 
convert(date,@FromDate) and convert(date,@ToDate)
and tbl_Grievance.intBomasid like @intBomasid
					    
					  
					
					   
  end                  
end




