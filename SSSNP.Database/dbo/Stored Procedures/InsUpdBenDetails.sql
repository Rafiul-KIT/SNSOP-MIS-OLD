CREATE Procedure [dbo].[InsUpdBenDetails]                  
(  
@SOurceofIncome varchar(70),    
@Age  int,        
@LegalStatus varchar(50),     
@Email varchar(50),                
@intBenid int,            
@intIP int,            
            
@NameofHousehold varchar(100),            
@FamilySize int,            
@Sex varchar(1),            
@NameofSpouse varchar(100),            
@NoofAdult int,            
@NoofDependents int,            
@FamilyIncome decimal(18, 2),            
@intStateid int,            
@intCountieid int,            
@intPayamid int,            
@intBomasid int,            
@Address varchar(150),            
@ContactNo varchar(15),            
@SecondLineHome varchar(15),            
@IsDisability varchar(1),            
@intDisability int,            
@BiometricNo varchar(50),            
@NameofBank varchar(50),            
@AccountNo varchar(15),  
@BomasCode  varchar(50),
@BeneficiaryType  varchar(50),
@GroupId  varchar(50), 
 @Name1stAlternatePayee varchar(100),
 @Name2ndAlternatePayee varchar(100),
 @NumberMaleAdults int, 
 @NumberFemaleAdults int, 
 @NumberAdultsAgeAbove65 int, 
 @NumberAdultsAgeBetween45_64 int, 
 @NumberAdultsAgeBetween18_30 int, 
 @NumberAdultsAgeBetween30_45 int, 
 @NumberFemaleChildren int, 
 @NumberMaleChildren int, 
 @NumberChildrenAgeAbove5years int, 
 @NumberChildrenAgeAbove12years int, 
 @NumberChildrenAgeAbove18years int, 
 @EthnicityTribeRgin varchar(100),       
@Created_by int,
@Name varchar(100),
@PhotoPath varchar(200),
@NationalID varchar(100)   ,
@SelectionCriteria nvarchar(200),
@SelectionReason nvarchar(max)
)                  
as begin  
--declare   @EnrollmentNo varchar(50)   

Declare @Prefix varchar(50);

Set @Prefix = Case When @BeneficiaryType = 'Group' Then @BomasCode + '/' + @GroupId + '/' Else @BomasCode + '/' + @BeneficiaryType + '/' End;

Declare @MaxEnrollmentNo varchar(50);

Select @MaxEnrollmentNo =  @Prefix + FORMAT(ISNULL(Max(CONVERT(int,SUBSTRING(EnrollmentNo,Len(@Prefix)+1,3))),0) + 1,'000') from tbl_BeneficiaryDet Where EnrollmentNo Like @Prefix + '%';         
                 
 -- SET @EnrollmentNo= @BomasCode +'/'+ (case when len(@Beneficiary)=1 then '0'+@Beneficiary else @Beneficiary end) +'/'+(case when len(@BeneficiaryNo)=1 then '00'+@BeneficiaryNo when len(@BeneficiaryNo)=2 then '0'+@BeneficiaryNo else @BeneficiaryNo end) 


  
  
              
if(@intBenid>0)                  
begin                  
if((select COUNT(*) from tbl_BeneficiaryDet where EnrollmentNo=@MaxEnrollmentNo and Email =@Email and intBomasid=@intBomasid and ContactNo=@ContactNo  and intBenid<> @intBenid)>0)                  
begin                  
select 999                  
return 999                  
end                  
else                  
begin   
 -- declare   @EnrollmentNo varchar(50)

 if((select COUNT(*) from tbl_BeneficiaryDet where intBenid<>@intBenid and NameofHousehold=@NameofHousehold and NameofSpouse=@NameofSpouse and  intBomasid=@intBomasid  )<>0)                  
 Begin
 select 999                  
return 999 
 End
 Else
 Begin
    if((select COUNT(*) from tbl_BeneficiaryDet where intBenid=@intBenid and BeneficiaryType=@BeneficiaryType and GroupId=@GroupId and intBomasid=@intBomasid )>0)   
begin         
update dbo.tbl_BeneficiaryDet  set SOurceofIncome=@SOurceofIncome, Age=@Age, LegalStatus=@LegalStatus, Email=@Email, intIP=@intIP, NameofHousehold=@NameofHousehold, FamilySize=@FamilySize, Sex=@Sex, NameofSpouse=@NameofSpouse, 

NoofAdult=@NoofAdult, NoofDependents=@NoofDependents, FamilyIncome=@FamilyIncome, intStateid=@intStateid, intCountieid=@intCountieid,             
                      intPayamid=@intPayamid, intBomasid=@intBomasid, Address=@Address, ContactNo=@ContactNo, SecondLineHome=@SecondLineHome, IsDisability=@IsDisability, intDisability=@intDisability, BiometricNo=@BiometricNo,  NameofBank=@NameofBank, AccountNo=@AccountNo,             
                        Modified_by=@Created_by, Modified_dt=GETDATE(),BeneficiaryType=@BeneficiaryType,GroupId=@GroupId,
Name1stAlternatePayee=@Name1stAlternatePayee,Name2ndAlternatePayee=@Name2ndAlternatePayee,NumberMaleAdults=@NumberMaleAdults,NumberFemaleAdults=@NumberFemaleAdults,NumberAdultsAgeAbove65=@NumberAdultsAgeAbove65,
NumberAdultsAgeBetween45_64=@NumberAdultsAgeBetween45_64,NumberAdultsAgeBetween18_30=@NumberAdultsAgeBetween18_30
,NumberAdultsAgeBetween30_45=@NumberAdultsAgeBetween30_45,NumberFemaleChildren=@NumberFemaleChildren,NumberMaleChildren=@NumberMaleChildren,
NumberChildrenAgeAbove5years=@NumberChildrenAgeAbove5years,NumberChildrenAgeAbove12years=@NumberChildrenAgeAbove12years,NumberChildrenAgeAbove18years=@NumberChildrenAgeAbove18years,
EthnicityTribeRgin=@EthnicityTribeRgin,PhotoPath = @PhotoPath,PhotoName = @Name,NationalID = @NationalID,
SelectionCriteria = @SelectionCriteria, SelectionReason = @SelectionReason
                 
where intBenid like @intBenid                  
select 1                  
return 1                  
end 
else
begin
update dbo.tbl_BeneficiaryDet  set SOurceofIncome=@SOurceofIncome, Age=@Age, LegalStatus=@LegalStatus, Email=@Email, intIP=@intIP, EnrollmentNo=@MaxEnrollmentNo, NameofHousehold=@NameofHousehold, FamilySize=@FamilySize, Sex=@Sex, NameofSpouse=@NameofSpouse, 

NoofAdult=@NoofAdult, NoofDependents=@NoofDependents, FamilyIncome=@FamilyIncome, intStateid=@intStateid, intCountieid=@intCountieid,             
                      intPayamid=@intPayamid, intBomasid=@intBomasid, Address=@Address, ContactNo=@ContactNo, SecondLineHome=@SecondLineHome, IsDisability=@IsDisability, intDisability=@intDisability, BiometricNo=@BiometricNo,  NameofBank=@NameofBank, AccountNo=@AccountNo,             
                        Modified_by=@Created_by, Modified_dt=GETDATE(),BeneficiaryType=@BeneficiaryType,GroupId=@GroupId,
						Name1stAlternatePayee=@Name1stAlternatePayee,Name2ndAlternatePayee=@Name2ndAlternatePayee,NumberMaleAdults=@NumberMaleAdults,NumberFemaleAdults=@NumberFemaleAdults,NumberAdultsAgeAbove65=@NumberAdultsAgeAbove65,
NumberAdultsAgeBetween45_64=@NumberAdultsAgeBetween45_64,NumberAdultsAgeBetween18_30=@NumberAdultsAgeBetween18_30
,NumberAdultsAgeBetween30_45=@NumberAdultsAgeBetween30_45,NumberFemaleChildren=@NumberFemaleChildren,NumberMaleChildren=@NumberMaleChildren,
NumberChildrenAgeAbove5years=@NumberChildrenAgeAbove5years,NumberChildrenAgeAbove12years=@NumberChildrenAgeAbove12years,NumberChildrenAgeAbove18years=@NumberChildrenAgeAbove18years,
EthnicityTribeRgin=@EthnicityTribeRgin,PhotoPath = @PhotoPath,PhotoName = @Name,NationalID = @NationalID,
SelectionCriteria = @SelectionCriteria, SelectionReason = @SelectionReason
                    
where intBenid like @intBenid                  
select 1                  
return 1  
end
end  
end               
end                  
else                  
begin                  
if((select COUNT(*) from tbl_BeneficiaryDet where Email=@Email and EnrollmentNo=@MaxEnrollmentNo and intBomasid=@intBomasid and ContactNo=@ContactNo )>0)                  
        
begin                  
select 999                  
return 999                  
end                  
else                  
begin   
 if((select COUNT(*) from tbl_BeneficiaryDet where NameofHousehold=@NameofHousehold and NameofSpouse=@NameofSpouse and  intBomasid=@intBomasid  )<>0)                  
 Begin
 select 999                  
return 999 
 End
 Else
 Begin               
insert into tbl_BeneficiaryDet(SOurceofIncome,Age,LegalStatus,Email, intIP, EnrollmentNo, NameofHousehold, FamilySize, Sex, NameofSpouse, NoofAdult, NoofDependents, FamilyIncome, intStateid, intCountieid,intPayamid, intBomasid, Address, ContactNo, SecondLineHome, IsDisability, intDisability, BiometricNo,  NameofBank, AccountNo,             
                       Created_by, Created_dt,BeneficiaryType,GroupId,
					   Name1stAlternatePayee,Name2ndAlternatePayee,NumberMaleAdults,NumberFemaleAdults,NumberAdultsAgeAbove65,
NumberAdultsAgeBetween45_64,NumberAdultsAgeBetween18_30
,NumberAdultsAgeBetween30_45,NumberFemaleChildren,NumberMaleChildren,
NumberChildrenAgeAbove5years,NumberChildrenAgeAbove12years,NumberChildrenAgeAbove18years,EthnicityTribeRgin,PhotoPath ,PhotoName,NationalID, SelectionCriteria, SelectionReason)                  
values(@SOurceofIncome,@Age,@LegalStatus,@Email,@intIP , @MaxEnrollmentNo , @NameofHousehold , @FamilySize , @Sex , @NameofSpouse , @NoofAdult , @NoofDependents , @FamilyIncome , @intStateid , @intCountieid , @intPayamid , @intBomasid , @Address , @ContactNo , @SecondLineHome ,   
@IsDisability   
        
          
 , @intDisability , @BiometricNo , @NameofBank , @AccountNo , @Created_by ,  GETDATE(),@BeneficiaryType,@GroupId,
 @Name1stAlternatePayee,@Name2ndAlternatePayee,@NumberMaleAdults,@NumberFemaleAdults,@NumberAdultsAgeAbove65,
@NumberAdultsAgeBetween45_64,@NumberAdultsAgeBetween18_30
,@NumberAdultsAgeBetween30_45,@NumberFemaleChildren,@NumberMaleChildren,
@NumberChildrenAgeAbove5years,@NumberChildrenAgeAbove12years,@NumberChildrenAgeAbove18years,@EthnicityTribeRgin,@PhotoPath,@Name,@NationalID, @SelectionCriteria, @SelectionReason)                  
select @@IDENTITY                  
return @@IDENTITY 
End                 
end                  
end                  
end




