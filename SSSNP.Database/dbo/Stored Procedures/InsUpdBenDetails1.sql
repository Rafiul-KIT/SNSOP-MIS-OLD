
CREATE Procedure [dbo].[InsUpdBenDetails1]                  
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
@NationalID varchar(100),            
@SelectionCriteria nvarchar(200),
@SelectionReason nvarchar(MAX)
)                  
as begin  
--declare   @EnrollmentNo varchar(50)   

Declare @Prefix varchar(50);

Set @Prefix = Case When @BeneficiaryType = 'Group' Then @BomasCode + '/' + @GroupId + '/' Else @BomasCode + '/' + @BeneficiaryType + '/' End;

Declare @MaxEnrollmentNo varchar(50);

Select @MaxEnrollmentNo =  @Prefix + FORMAT(ISNULL(Max(CONVERT(int,SUBSTRING(EnrollmentNo,Len(@Prefix)+1,3))),0) + 1,'000') from tbl_BeneficiaryDet Where EnrollmentNo Like @Prefix + '%';         
                 
 -- SET @EnrollmentNo= @BomasCode +'/'+ (case when len(@Beneficiary)=1 then '0'+@Beneficiary else @Beneficiary end) +'/'+(case when len(@BeneficiaryNo)=1 then '00'+@BeneficiaryNo when len(@BeneficiaryNo)=2 then '0'+@BeneficiaryNo else @BeneficiaryNo end) 


  
  
      Declare @household_number as varchar(80)
	  set    @household_number=(select household_number from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)     
                
insert into tbl_BeneficiaryDet(SOurceofIncome,Age,LegalStatus,Email, intIP, EnrollmentNo, NameofHousehold, FamilySize, Sex, NameofSpouse, NoofAdult, NoofDependents, FamilyIncome, intStateid, intCountieid,intPayamid, intBomasid, Address, ContactNo, SecondLineHome, IsDisability, intDisability, BiometricNo,  NameofBank, AccountNo,             
                       Created_by, Created_dt,BeneficiaryType,GroupId,
					   Name1stAlternatePayee,Name2ndAlternatePayee,NumberMaleAdults,NumberFemaleAdults,NumberAdultsAgeAbove65,
NumberAdultsAgeBetween45_64,NumberAdultsAgeBetween18_30
,NumberAdultsAgeBetween30_45,NumberFemaleChildren,NumberMaleChildren,
NumberChildrenAgeAbove5years,NumberChildrenAgeAbove12years,NumberChildrenAgeAbove18years,EthnicityTribeRgin,PhotoPath ,PhotoName,NationalID,household_number, SelectionCriteria, SelectionReason)                  
values(@SOurceofIncome,@Age,@LegalStatus,@Email,@intIP , @MaxEnrollmentNo , @NameofHousehold , @FamilySize , @Sex , @NameofSpouse , @NoofAdult , @NoofDependents , @FamilyIncome , @intStateid , @intCountieid , @intPayamid , @intBomasid , @Address , @ContactNo , @SecondLineHome ,   
@IsDisability   
        
          
 , @intDisability , @BiometricNo , @NameofBank , @AccountNo , @Created_by ,  GETDATE(),@BeneficiaryType,@GroupId,
 @Name1stAlternatePayee,@Name2ndAlternatePayee,@NumberMaleAdults,@NumberFemaleAdults,@NumberAdultsAgeAbove65,
@NumberAdultsAgeBetween45_64,@NumberAdultsAgeBetween18_30
,@NumberAdultsAgeBetween30_45,@NumberFemaleChildren,@NumberMaleChildren,
@NumberChildrenAgeAbove5years,@NumberChildrenAgeAbove12years,@NumberChildrenAgeAbove18years,@EthnicityTribeRgin,@PhotoPath,@Name,@NationalID,@household_number, @SelectionCriteria, @SelectionReason)                  
select @@IDENTITY                  
return @@IDENTITY                  
                 
end




