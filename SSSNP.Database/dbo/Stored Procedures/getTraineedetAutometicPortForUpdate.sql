CREATE Procedure [dbo].[getTraineedetAutometicPortForUpdate]                      
--Jyotshna On 21-08-2013                      
--@IPname varchar(60),                      
@Enrollmentno varchar(50),                      
@householdname varchar(100) ,
@HouseholdSize varchar(8), 
@SpouseName varchar(100),
@Dependentsno varchar(8), 
@Name1stAlternatePayee varchar(100),
 @Name2ndAlternatePayee varchar(100)           
as                      
begin                  
select * from tbl_BeneficiaryDet where EnrollmentNo != @Enrollmentno and NameofHousehold like @householdname 
and NameofHousehold=@HouseholdName and FamilySize=@HouseholdSize and NameofSpouse=@SpouseName 
and NoofDependents=@Dependentsno and Name1stAlternatePayee=@Name1stAlternatePayee and Name2ndAlternatePayee=@Name2ndAlternatePayee

--traineeName like @traineeName and traineeFather like @traineeFather                 
--and traineeDOB=@traineeDOB              
--Jyotshna On 31-07-2013            
end




