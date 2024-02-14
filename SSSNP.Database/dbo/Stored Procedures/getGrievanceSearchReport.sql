CREATE Procedure [dbo].[getGrievanceSearchReport]               
(    
	@FromDate  date ,
	@ToDate  date
)                    
as begin                    
                    
 if(@FromDate is null and @ToDate is null)
 begin
 SELECT   tbl_Grievance.intBomasid,
tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, cast(@FromDate as date) as FDate,@ToDate as TDate, 
tm_States.StateName,[dbo].[GetCount_Sp](tm_Bomas.intBomasid) as NoOfReg  



					              
FROM         tbl_Grievance INNER JOIN                    
                      tm_States on tm_States.intStateid=tbl_Grievance.intStateid      
inner join tm_Counties  on tm_Counties.intCountieid=tbl_Grievance.intCountieid      
inner join tm_Payams on tm_Payams.intPayamid=tbl_Grievance.intPayamid      
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_Grievance.intBomasid  

  group by tbl_Grievance.intBomasid
					  ,tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
tm_States.StateName ,tm_Bomas.intBomasid 
 end                                   
 else
 begin                   
SELECT  tbl_Grievance.intBomasid,
tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, cast(@FromDate as date) as FDate,@ToDate as TDate, 
tm_States.StateName,[dbo].[GetCount_Sp](tm_Bomas.intBomasid) as NoOfReg  



					              
FROM         tbl_Grievance INNER JOIN                    
                      tm_States on tm_States.intStateid=tbl_Grievance.intStateid      
inner join tm_Counties  on tm_Counties.intCountieid=tbl_Grievance.intCountieid      
inner join tm_Payams on tm_Payams.intPayamid=tbl_Grievance.intPayamid      
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_Grievance.intBomasid  
                                          
                      where convert(date,tbl_Grievance.ComplaintRegisteredDate) between 
convert(date,@FromDate) and convert(date,@ToDate)
					    
					  
					  group by tbl_Grievance.intBomasid
					  ,tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
tm_States.StateName,tm_Bomas.intBomasid  
                     
					   
 end                   
end




