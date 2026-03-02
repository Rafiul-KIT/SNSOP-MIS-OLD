CREATE procedure [dbo].[UpdateBenficiariesEnrollment] -- UpdateBenficiariesEnrollmentOLD '20014'
(
@intBenid varchar(8)
)
AS
Begin

Declare @intBomaidPreName as varchar(180)
set @intBomaidPreName=(select NameofHousehold from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)


Declare @intBomaidPreSpouse as varchar(180)
set @intBomaidPreSpouse=(select NameofSpouse from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)

Declare @intBomaidNew as varchar(180)
set @intBomaidNew=(select intBomasid from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)


Declare @householdno as varchar(180)
set @householdno=(select household_number from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)


if((select count(*) from tbl_BeneficiaryServiceDet where  intBenTempid like @intBenid and BenStatus='2')<>0)
Begin
select 100
return 100
End
Else




Begin

if((select count(*) from tbl_BeneficiaryDet where   tbl_BeneficiaryDet.household_number like @householdno and tbl_BeneficiaryDet.NameofHousehold like @intBomaidPreName and tbl_BeneficiaryDet.NameofSpouse like @intBomaidPreSpouse and tbl_BeneficiaryDet.intBomasid like @intBomaidNew )<>0)
Begin
select 100
return 100
End
Else
Begin
Declare @intBomaidPre as varchar(10)
set @intBomaidPre=(select intBomasid from tbl_BeneficiaryServiceDet where intBenTempid like @intBenid)





Declare @cnt as int
set @cnt=(select count(*) from  tbl_BeneficiaryDet where intBomasid like @intBomaidPre and BeneficiaryType not like '%CST%'  and EnrollmentNo not like '%CST%')

Declare @ids as int

insert into tbl_BeneficiaryDet(intIP,OLDEnrollmentNo,EnrollmentNo,NameofHousehold,FamilySize,Sex,Age,NameofSpouse,
NoofAdult,NoofDependents,FamilyIncome,intStateid,intCountieid,intPayamid,intBomasid,Address,ContactNo,Email,SecondLineHome,
IsDisability,BiometricNo,NameofBank,AccountNo,WorkCode,LegalStatus,Created_by,Created_dt,SOurceofIncome,RegYear,PortYear,
PortPhase,PortedSheetName,BDropout,DropoutRemarks,GroupId,BeneficiaryType,Name1stAlternatePayee,Name2ndAlternatePayee,
NumberMaleAdults,NumberFemaleAdults,NumberAdultsAgeAbove65,NumberAdultsAgeBetween45_64,NumberAdultsAgeBetween18_30,NumberAdultsAgeBetween30_45,
NumberFemaleChildren,NumberMaleChildren,NumberChildrenAgeAbove5years,NumberChildrenAgeAbove12years,NumberChildrenAgeAbove18years,
EthnicityTribeRgin,PhotoPath,PhotoName,NationalID,PCPortFlag,household_number,FirstAlternatePayeeGender,FirstAlternatePayeeAge,
FirstAlternatePayeeNationID,FirstAlternatePayeeMobile,FirstAlternatePayeePhotoName,FirstAlternatePayeePhotoPath,SecondAlternatePayeeGender,
SecondAlternatePayeeAge,SecondAlternateNationalID,SecondAlternatePayeeMobile,SecondAlternatepayeePhotoName,SecondAlternatePayeePhotoPath,
supervisor_id,member_number,female_dependants,male_dependants,latitude,longitude,id_url,TransferDate,TransferUpdateat,alternate_number,
Secondalternate_number,API_Status,SelectionCriteria, SelectionReason)


select  intIP,OLDEnrollmentNo,NULL,NameofHousehold,FamilySize,Sex,Age,NameofSpouse,
NoofAdult,NoofDependents,FamilyIncome,intStateid,intCountieid,intPayamid,intBomasid,Address,ContactNo,Email,SecondLineHome,
IsDisability,BiometricNo,NameofBank,AccountNo,WorkCode,LegalStatus,Created_by,getdate(),SOurceofIncome,RegYear,PortYear,
PortPhase,PortedSheetName,BDropout,DropoutRemarks,null,BeneficiaryType,Name1stAlternatePayee,Name2ndAlternatePayee,
NumberMaleAdults,NumberFemaleAdults,NumberAdultsAgeAbove65,NumberAdultsAgeBetween45_64,NumberAdultsAgeBetween18_30,NumberAdultsAgeBetween30_45,
NumberFemaleChildren,NumberMaleChildren,NumberChildrenAgeAbove5years,NumberChildrenAgeAbove12years,NumberChildrenAgeAbove18years,
EthnicityTribeRgin,PhotoPath,PhotoName,NationalID,PCPortFlag,household_number,FirstAlternatePayeeGender,FirstAlternatePayeeAge,
FirstAlternatePayeeNationID,FirstAlternatePayeeMobile,FirstAlternatePayeePhotoName,FirstAlternatePayeePhotoPath,SecondAlternatePayeeGender,
SecondAlternatePayeeAge,SecondAlternateNationalID,SecondAlternatePayeeMobile,SecondAlternatepayeePhotoName,SecondAlternatePayeePhotoPath,
supervisor_id,member_number,female_dependants,male_dependants,latitude,longitude,id_url,TransferDate,TransferUpdateat,alternate_number,
Secondalternate_number,API_Status,SelectionCriteria, SelectionReason
from tbl_BeneficiaryServiceDet where intBenTempid  like @intBenid

set @ids=@@IDENTITY

Update tbl_BeneficiaryServiceDet set BenStatus='2', Modified_dt=getdate(), Modified_by=1000 where intBenTempid  like @intBenid

Declare @BomaCode as varchar(10)
Declare @intBomaid as varchar(10)
set @intBomaid=(select intBomasid from tbl_BeneficiaryDet where intBenid like @ids)
set @BomaCode=(select b.BomaCode from tm_Bomas as b where b.intBomasid like @intBomaid)
Declare @Groupid as varchar(10)
set @Groupid=(select GroupId from tbl_BeneficiaryDet where intBenid like @ids)
Declare @GroupType as varchar(10)
set @GroupType=(select BeneficiaryType from tbl_BeneficiaryDet where intBenid like @ids)
--if((select count(*) from tbl_BeneficiaryDet where intBenid like @ids and GroupId like '%CST%')=1)
if(@GroupType='CST')
Begin
if((select  count(*) from tbl_BeneficiaryDet where intBomasid like @intBomaid and BeneficiaryType like '%CST%' )  =1)
Begin
Update tbl_BeneficiaryDet set EnrollmentNo= @BomaCode +'/' + 'CST'   +'/' + '001', GroupId=NULL where intBenid like @ids
End
else
Begin
Declare @countBensGroupidCST as int
set @countBensGroupidCST=(select  count(*) from tbl_BeneficiaryDet where intBomasid like @intBomaid and BeneficiaryType like '%CST%' )
Declare @BenidsCSR as varchar(8)
set @countBensGroupidCST=@countBensGroupidCST+1
set @BenidsCSR= Case when LEN(@countBensGroupidCST)=1 then '00' + convert(varchar, @countBensGroupidCST) else Case when LEN(@countBensGroupidCST)=2 then '0' + convert(varchar, @countBensGroupidCST) else convert(varchar, @countBensGroupidCST)  end end


Update tbl_BeneficiaryDet set EnrollmentNo= @BomaCode +'/' + 'CST'   +'/' + @BenidsCSR, GroupId=NULL where intBenid like @ids
End

End
Else
Begin



if(@cnt=0)
Begin

Update tbl_BeneficiaryDet set EnrollmentNo= @BomaCode +'/' + '01' +'/' + '001', GroupId='01' where intBenid like @ids
End
else
Begin

Declare @GroupidMAX as int
set @GroupidMAX=(select MAX(Convert(int,isnull(GroupId,1))) from tbl_BeneficiaryDet where intBomasid like @intBomaid AND GroupId IS NOT NULL  and BeneficiaryType not like '%CST%' and EnrollmentNo not like '%CST%' )
Declare @UpdateGroupidAA as varchar(8)
set @UpdateGroupidAA= case when LEN(@GroupidMAX)=1 then '0'+ Convert(varchar,@GroupidMAX) else Convert(varchar,@GroupidMAX) end
Declare @A1 as int
Declare @A2 as int
set @A1=(select  count(*) from tbl_BeneficiaryDet where intBomasid like @intBomaid and Convert(int,isnull(GroupId,0)) like @GroupidMAX AND GroupId IS NOT NULL and BeneficiaryType not like '%CST%'  and EnrollmentNo not like '%CST%' ) 
set @A2=(select top 1  GroupStrength from GroupMaster where Convert(int,isnull(GroupCode,0)) like @GroupidMAX and Boma like @intBomaid and GroupCode not like '%CST%'  and GroupType not like '%CST%')



if( @A1>=@A2 )
Begin

set @GroupidMAX=@GroupidMAX+1
Declare @UpdateGroupid as varchar(8)
set @UpdateGroupid= case when LEN(@GroupidMAX)=1 then '0'+ Convert(varchar,@GroupidMAX) else Convert(varchar,@GroupidMAX) end
Update tbl_BeneficiaryDet set EnrollmentNo= @BomaCode +'/' + @UpdateGroupid   +'/' + '001', GroupId=@UpdateGroupid where intBenid like @ids

End
else 
Begin

Declare @UpdateGroupida as varchar(8)
set @UpdateGroupida= case when LEN(@GroupidMAX)=1 then '0'+ Convert(varchar,@GroupidMAX) else Convert(varchar,@GroupidMAX) end

Declare @countBensGroupid as int
set @countBensGroupid=(select  count(*) from tbl_BeneficiaryDet where intBomasid like @intBomaid and Convert(int,isnull(GroupId,0)) like @GroupidMAX and GroupId is not null and BeneficiaryType not like '%CST%'   and EnrollmentNo not like '%CST%')
Declare @Benids as varchar(8)
set @countBensGroupid=@countBensGroupid+1
set @Benids= Case when LEN(@countBensGroupid)=1 then '00' + convert(varchar, @countBensGroupid) else Case when LEN(@countBensGroupid)=2 then '0' + convert(varchar, @countBensGroupid) else convert(varchar, @countBensGroupid)  end end


Update tbl_BeneficiaryDet set EnrollmentNo= @BomaCode +'/' + @UpdateGroupida   +'/' + @Benids, GroupId=@UpdateGroupida where intBenid like @ids
End


End

End



select @ids
return @ids
End
End
End

