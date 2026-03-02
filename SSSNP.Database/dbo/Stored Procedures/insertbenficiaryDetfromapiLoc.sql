CREATE procedure [dbo].[insertbenficiaryDetfromapiLoc]
  
	@intIP	varchar(100),
	@household_number	varchar(100),
	@BeneficiaryType	varchar(100),
	@GroupId	varchar(100),
	@supervisor_id	varchar(100),
	@member_number	varchar(100),
	@FamilySize	varchar(100),
	@NameofHousehold	varchar(100),
	@Age	varchar(100),
	@NameofSpouse	varchar(100),
	@NationalID	varchar(100),
	@ContactNo	varchar(100),
	@Sex	varchar(1),
	@female_dependants	varchar(100),
	@male_dependants	varchar(100),
	@NoofDependents	varchar(100),
	@NumberChildrenAgeAbove5years	varchar(100),
	@NumberChildrenAgeAbove18years	varchar(100),
	@NumberAdultsAgeBetween30_45	varchar(100),
	@NumberAdultsAgeBetween45_64	varchar(100),
	@NumberAdultsAgeAbove65	varchar(100),
	@SOurceofIncome	varchar(100),
	@FamilyIncome	varchar(100),
	@intStateid	varchar(100),
	@intCountieid	varchar(100),
	@intPayamid	varchar(100),
	@intBomasid	varchar(100),
	@latitude	varchar(100),
	@longitude	varchar(100),
	@LegalStatus	varchar(100),
	@PhotoName	varchar(100),	
	@id_url	varchar(100),
	@TransferDate	varchar(100),
	@TransferUpdateat	varchar(100),	
	@Created_by varchar(100),	
	@SelectionCriteria varchar(100),	
	@SelectionReason varchar(100),	
	@ReturnVal int output
  
as begin  
   
Declare @BomasCode  varchar(50),@exist_household_number nvarchar(200),@exist_servicehousehold_number nvarchar(200)
 
 set @BomasCode=(select top 1 BomaCode from tm_Bomas where intBomasid =@intBomasid)
 set @exist_household_number = (select top 1  household_number from tbl_BeneficiaryDetLoc where household_number = @household_number)
  set @exist_servicehousehold_number = (select top 1 household_number from tbl_BeneficiaryServiceDetLoc where household_number = @household_number)
  Declare @checkcnt as int
  set @checkcnt=1
  if(@intIP <>'' and @intIP <>'null' )
 begin
 set @checkcnt=(select count(*) from td_IPDetails as ip where ip.intIP=@intIP)
 End
 if (@BomasCode is null)
 begin 
   set @ReturnVal =  98        
  
 end
 else if(@exist_household_number!='')
 begin
	 set @ReturnVal =  99   
 end
  else if(@exist_servicehousehold_number!='')
 begin
	 set @ReturnVal =  97   
 end
else if(@intIP ='' or @intIP ='null')
 begin
	 set @ReturnVal =  96   
 end
 else if(@BomasCode ='' or @BomasCode ='null')
 begin
	 set @ReturnVal =  95   
 end
 else if(@household_number ='' or @household_number ='null')
 begin
	 set @ReturnVal =  94   
 end
  else if(@intStateid ='' or @intStateid ='null')
 begin
	 set @ReturnVal =  93   
 end
  else if(@intCountieid ='' or @intCountieid ='null')
 begin
	 set @ReturnVal =  92   
 end
  else if(@intPayamid ='' or @intPayamid ='null')
 begin
	 set @ReturnVal =  91   
 end
  else if(@intBomasid ='' or @intBomasid ='null')
 begin
	 set @ReturnVal =  85   
 end
  else if(@NameofHousehold ='' or @NameofHousehold ='null')
 begin
	 set @ReturnVal =  90   
 end
 else if(@intIP <>'' and @intIP <>'null' and @checkcnt=0)
 begin
 
	 set @ReturnVal =  89 
	  
 end
  else if(@intBomasid <>'' and @intBomasid <>'null' and @intPayamid <>'' and (select top 1 intPayamid from tm_Bomas where intBomasid like @intBomasid) <> @intPayamid )
 begin
 
	 set @ReturnVal =  88 
 
	  
 end
   else if(@intPayamid <>'' and @intCountieid <>'' and (select top 1 intCountieid from tm_Payams where intPayamid like @intPayamid) <> @intCountieid )
 begin
 
	 set @ReturnVal =  87 
 
	  
 end
  else if(@intCountieid <>'' and @intStateid <>'' and (select top 1 intStateid from tm_Counties where intCountieid like @intCountieid) <> @intStateid )
 begin
 
	 set @ReturnVal =  86 
 
	  
 end
 else
 begin
	
	Declare @Prefix varchar(50) 
Set @Prefix = Case When @BeneficiaryType = 'Group' Then @BomasCode + '/' + @GroupId + '/' Else @BomasCode + '/' + @BeneficiaryType + '/' End;

Declare @MaxEnrollmentNo varchar(50);
--Select @MaxEnrollmentNo =  @Prefix + FORMAT(ISNULL(Max(CONVERT(int,SUBSTRING(EnrollmentNo,Len(@Prefix)+1,3))),0) + 1,'000') from tbl_BeneficiaryDet Where EnrollmentNo Like @Prefix + '%'; 
Select @MaxEnrollmentNo =  @BomasCode  + REPLACE( REPLACE( REPLACE(CONVERT(VARCHAR(23), SYSDATETIME(), 121),':',''),'.',''),'-','')
 
    insert into tbl_BeneficiaryServiceDetLoc              
(              
 
        intIP,
household_number,
EnrollmentNo,
BeneficiaryType,
GroupId,
supervisor_id,
member_number,
FamilySize,
NameofHousehold,
Age,
NameofSpouse,
NationalID,
ContactNo,
Sex,
female_dependants,
male_dependants,
NoofDependents,
NumberChildrenAgeAbove5years,
NumberChildrenAgeAbove18years,
NumberAdultsAgeBetween30_45,
NumberAdultsAgeBetween45_64,
NumberAdultsAgeAbove65,
SOurceofIncome,
FamilyIncome,
intStateid,
intCountieid,
intPayamid,
intBomasid,
latitude,
longitude,
LegalStatus,
PhotoName,
id_url,
TransferDate,
TransferUpdateat,Created_by,API_Status	,
SelectionCriteria,
SelectionReason

  
)    
   values  
     
   (  
  
		@intIP,
@household_number,
@MaxEnrollmentNo,
@BeneficiaryType,
@GroupId,
@supervisor_id,
@member_number,
@FamilySize,
@NameofHousehold,
@Age,
@NameofSpouse,
@NationalID,
@ContactNo,
@Sex,
@female_dependants,
@male_dependants,
@NoofDependents,
@NumberChildrenAgeAbove5years,
@NumberChildrenAgeAbove18years,
@NumberAdultsAgeBetween30_45,
@NumberAdultsAgeBetween45_64,
@NumberAdultsAgeAbove65,
@SOurceofIncome,
@FamilyIncome,
@intStateid,
@intCountieid,
@intPayamid,
@intBomasid,
@latitude,
@longitude,
@LegalStatus,
@PhotoName,
@id_url,
@TransferDate,
@TransferUpdateat,
@Created_by,'Success'	,
@SelectionCriteria,
@SelectionReason

     
   )  
   Declare @ids  as int          
	set @ids=@@IDENTITY
	Update tbl_BeneficiaryServiceDetLoc set EnrollmentNo=@MaxEnrollmentNo+Convert(varchar,@ids) where intBenid like @ids
   set @ReturnVal = @ids

 end

 return @ReturnVal

 end