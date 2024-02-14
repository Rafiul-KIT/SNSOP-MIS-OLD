CREATE Procedure [dbo].[DeleteBenficiarynew]
(
@intBenid varchar(8),
@DeletedBy varchar(8)
)
As
-- Created by : Srikanth
-- Created dt : 10-11-2017
-- Purpose : Deletion for Benficiary
-- Tables Used : tbl_BeneficiaryDet,tr_BenfWorkDet,tr_BenAttendanceDet,tr_BenWagesPayment
Begin

if((select COUNT(*) from tr_BenWagesPayment where intBenid like @intBenid) <> 0 )
Begin
select 111
return 111
End
else if((select COUNT(*) from tr_BenAttendanceDet where intBenid like @intBenid) <> 0 )
Begin
select 111
return 111
End
else if((select COUNT(*) from tr_BenfWorkDet where intBenid like @intBenid) <> 0 )
Begin
select 111
return 111
End
else
Begin
insert into  tbl_BeneficiaryDetTrans
(
 intBenid, intIP, OLDEnrollmentNo, EnrollmentNo, NameofHousehold, FamilySize, Sex, Age, NameofSpouse, NoofAdult, NoofDependents, 
                      FamilyIncome, intStateid, intCountieid, intPayamid, intBomasid, Address, ContactNo, Email, SecondLineHome, IsDisability, intDisability, BiometricNo, NameofBank, 
                      AccountNo, WorkCode, LegalStatus, Created_by, Created_dt, Modified_by, Modified_dt, SOurceofIncome, RegYear, PortYear, PortPhase, PortedSheetName, BDropout, 
                      DropoutRemarks, GroupId, BeneficiaryType, Name1stAlternatePayee, Name2ndAlternatePayee, NumberMaleAdults, NumberFemaleAdults, NumberAdultsAgeAbove65, 
                      NumberAdultsAgeBetween45_64, NumberAdultsAgeBetween18_30, NumberAdultsAgeBetween30_45, NumberFemaleChildren, NumberMaleChildren, 
                      NumberChildrenAgeAbove5years, NumberChildrenAgeAbove12years, NumberChildrenAgeAbove18years, EthnicityTribeRgin, PhotoPath, PhotoName, NationalID, 
                      PCPortFlag, household_number, FirstAlternatePayeeGender, FirstAlternatePayeeAge, FirstAlternatePayeeNationID, FirstAlternatePayeeMobile, 
                      FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath, SecondAlternatePayeeGender, SecondAlternatePayeeAge, SecondAlternateNationalID, 
                      SecondAlternatePayeeMobile, SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath, supervisor_id, member_number, female_dependants, 
                      male_dependants, latitude, longitude, id_url, TransferDate, TransferUpdateat, alternate_number, Secondalternate_number, API_Status, DeletedBy, DeletedDt

)
select intBenid, intIP, OLDEnrollmentNo, EnrollmentNo, NameofHousehold, FamilySize, Sex, Age, NameofSpouse, NoofAdult, NoofDependents, 
                      FamilyIncome, intStateid, intCountieid, intPayamid, intBomasid, Address, ContactNo, Email, SecondLineHome, IsDisability, intDisability, BiometricNo, NameofBank, 
                      AccountNo, WorkCode, LegalStatus, Created_by, Created_dt, Modified_by, Modified_dt, SOurceofIncome, RegYear, PortYear, PortPhase, PortedSheetName, BDropout, 
                      DropoutRemarks, GroupId, BeneficiaryType, Name1stAlternatePayee, Name2ndAlternatePayee, NumberMaleAdults, NumberFemaleAdults, NumberAdultsAgeAbove65, 
                      NumberAdultsAgeBetween45_64, NumberAdultsAgeBetween18_30, NumberAdultsAgeBetween30_45, NumberFemaleChildren, NumberMaleChildren, 
                      NumberChildrenAgeAbove5years, NumberChildrenAgeAbove12years, NumberChildrenAgeAbove18years, EthnicityTribeRgin, PhotoPath, PhotoName, NationalID, 
                      PCPortFlag, household_number, FirstAlternatePayeeGender, FirstAlternatePayeeAge, FirstAlternatePayeeNationID, FirstAlternatePayeeMobile, 
                      FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath, SecondAlternatePayeeGender, SecondAlternatePayeeAge, SecondAlternateNationalID, 
                      SecondAlternatePayeeMobile, SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath, supervisor_id, member_number, female_dependants, 
                      male_dependants, latitude, longitude, id_url, TransferDate, TransferUpdateat, alternate_number, Secondalternate_number, API_Status,@DeletedBy, getdate()
 from  tbl_BeneficiaryDet where intBenid like @intBenid


 insert into tr_BenfWorkDetTrans
 (
  intBenfWorkid, intBenid, intWorkid, Created_by, Created_dt, Modified_by, Modifieid_dt, PortYear, PortPhase, PortedSheetName, 
                      Deleted_by, Deleted_dt

 )
 select   intBenfWorkid, intBenid, intWorkid, Created_by, Created_dt, Modified_by, Modifieid_dt, PortYear, PortPhase, PortedSheetName, 
                      @DeletedBy, getdate()
					  from tr_BenfWorkDet
where intBenid like @intBenid
Delete from tr_BenfWorkDet where intBenid like @intBenid
Delete from tbl_BeneficiaryDet where intBenid like @intBenid

select 1
return 1
end
End



