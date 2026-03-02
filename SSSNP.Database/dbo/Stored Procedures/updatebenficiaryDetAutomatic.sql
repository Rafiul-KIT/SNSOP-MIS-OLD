
CREATE procedure [dbo].[updatebenficiaryDetAutomatic] 
  
@IPName varchar(60), 
@HouseholdName varchar(100),  
@HouseholdSize varchar(8),  
@Sex varchar(1),  
@Age varchar(8),  
@SpouseName varchar(100),  
@Audltsno varchar(8),  
@Dependentsno varchar(8),  
@income varchar(20),  
@State varchar(80),  
@County varchar(80),  
@payam varchar(80),  
@boma varchar(80),  
@address varchar(150),  
@Contactno varchar(15),  
@Email varchar(50),  
@Secondhome varchar(15),  
@legalStatus varchar(50),  
@biometricno varchar(50),  
@bankname varchar(50),  
@Accno varchar(15),  
@TraineeSNo varchar(30), 

@EnrollmentNo  varchar(50),
 @Name1stAlternatePayee varchar(100),
 @Name2ndAlternatePayee varchar(100),
 @NumberMaleAdults varchar(60), 
 @NumberFemaleAdults varchar(60), 
 @NumberAdultsAgeAbove65 varchar(60), 
 @NumberAdultsAgeBetween45_64 varchar(60), 
 @NumberAdultsAgeBetween18_30 varchar(60), 
 @NumberAdultsAgeBetween30_45 varchar(60), 
 @NumberFemaleChildren varchar(60), 
 @NumberMaleChildren varchar(60), 
 @NumberChildrenAgeAbove5years varchar(60), 
 @NumberChildrenAgeAbove12years varchar(60), 
 @NumberChildrenAgeAbove18years varchar(60), 
 @EthnicityTribeRgin varchar(100),  
@created_by varchar(8),
@FirstAlternatePayeeGender varchar(1),
  @FirstAlternatePayeeAge varchar(8),
  @FirstAlternatePayeeNationID varchar(20),
  @FirstAlternatePayeeMobile varchar(12),
   @SecondAlternatePayeeGender varchar(1),
  @SecondAlternatePayeeAge varchar(8),
  @SecondAlternateNationalID varchar(20),
  @SecondAlternatePayeeMobile varchar(12)  
  
as begin  
  
  
--if((select count(*) from  td_IPDetails where   IPName like @IPName)<>0)              
--Begin              
--set @IPName=(select top 1 intIP from  td_IPDetails where  IPName like @IPName)              
--End              
--Else              
--Begin           
--select 100              
--return 100              
--End  
  
  
--if((select count(*) from  tm_States where   StateName like @State)<>0)              
--Begin              
--set @State=(select top 1 intStateid from  tm_States where  StateName like @State)              
--End              
--Else              
--Begin           
--select 102              
--return 102              
--End  
  
--if((select count(*) from  tm_Counties where   CountieName like @County)<>0)              
--Begin              
--set @County=(select top 1 intCountieid from  tm_Counties where  CountieName like @County)              
--End              
--Else              
--Begin              
--select 103              
--return 103              
--End              
  
  
  
--if((select count(*) from  tm_Payams where   PayamName like @payam)<>0)              
--Begin              
--set @payam=(select top 1 intPayamid from  tm_Payams where  PayamName like @payam)              
--End              
--Else              
--Begin              
--select 104              
--return 104              
--End              
  
  
--if((select count(*) from  tm_Bomas where   BomaName like @boma)<>0)              
--Begin              
--set @boma=(select top 1 intPayamid from  tm_Bomas where  BomaName like @boma)              
--End              
--Else              
--Begin              
--select 105              
--return 105              
--End    

 begin                  
if((select COUNT(*) from tbl_BeneficiaryDet where NameofHousehold=@HouseholdName and FamilySize=@HouseholdSize and NameofSpouse=@SpouseName and NoofDependents=@Dependentsno and Name1stAlternatePayee=@Name1stAlternatePayee and Name2ndAlternatePayee=@Name2ndAlternatePayee and EnrollmentNo != @EnrollmentNo)>0)                  
        
begin                  
select 99                  
return 99                  
end                  
else                  

  begin 


  update tbl_BeneficiaryDet              
set            
              
intIP=@IPName, 
NameofHousehold=@HouseholdName,  
FamilySize=@HouseholdSize,  
Sex=@Sex,  
Age=@Age,  
NameofSpouse=@SpouseName,  
 NoofAdult=@Audltsno,  
  NoofDependents=@Dependentsno,  
      FamilyIncome=@income,  
      intStateid=@State,  
     intCountieid=@County,  
      intPayamid=@payam,  
       intBomasid=@boma,  
       Address=@address,  
        ContactNo=@Contactno,  
         Email=@Email,  
         SecondLineHome=@Secondhome,  
         IsDisability='N',  
         BiometricNo=@biometricno,  
         NameofBank=@bankname,  
         AccountNo=@Accno,  
        LegalStatus=@legalStatus, Modified_by=@created_by,  
        Modified_dt=GETDATE() ,
					   Name1stAlternatePayee=@Name1stAlternatePayee,Name2ndAlternatePayee=@Name2ndAlternatePayee,
					   NumberMaleAdults=@NumberMaleAdults,NumberFemaleAdults=@NumberFemaleAdults,NumberAdultsAgeAbove65=@NumberAdultsAgeAbove65,
NumberAdultsAgeBetween45_64=@NumberAdultsAgeBetween45_64,NumberAdultsAgeBetween18_30=@NumberAdultsAgeBetween18_30
,NumberAdultsAgeBetween30_45=@NumberAdultsAgeBetween30_45,NumberFemaleChildren=@NumberFemaleChildren,NumberMaleChildren=@NumberMaleChildren,
NumberChildrenAgeAbove5years=@NumberChildrenAgeAbove5years,NumberChildrenAgeAbove12years=@NumberChildrenAgeAbove12years,
NumberChildrenAgeAbove18years=@NumberChildrenAgeAbove18years,EthnicityTribeRgin=@EthnicityTribeRgin,
FirstAlternatePayeeGender=@FirstAlternatePayeeGender, FirstAlternatePayeeAge=@FirstAlternatePayeeAge, FirstAlternatePayeeNationID=@FirstAlternatePayeeNationID, 
                         FirstAlternatePayeeMobile=@FirstAlternatePayeeMobile, 
						 SecondAlternatePayeeGender=@SecondAlternatePayeeGender, SecondAlternatePayeeAge=@SecondAlternatePayeeAge, 
						 SecondAlternateNationalID=@SecondAlternateNationalID, SecondAlternatePayeeMobile=@SecondAlternatePayeeMobile
where EnrollmentNo=@EnrollmentNo
        
  
   Declare @ids  as int          
set @ids=@@IDENTITY           
     
     
    select 1          
 return 1  

 end

end

  
end




