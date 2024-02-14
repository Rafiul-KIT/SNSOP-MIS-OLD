CREATE Procedure [dbo].[getTraineedetAutometicPort]                      
--Jyotshna On 21-08-2013                      
--@IPname varchar(60),                      
@Enrollmentno varchar(50),                      
@householdname varchar(100)          
as                      
begin                  
select * from tbl_BeneficiaryDet where EnrollmentNo like @Enrollmentno and NameofHousehold like @householdname

--traineeName like @traineeName and traineeFather like @traineeFather                 
--and traineeDOB=@traineeDOB              
--Jyotshna On 31-07-2013            
end




