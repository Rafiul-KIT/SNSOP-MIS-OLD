CREATE Procedure [dbo].[DeleteBenficiary1]
(
@intBenid varchar(8),
@Remarks varchar(350)
)
As
-- Created by : Srikanth
-- Created dt : 10-11-20171
-- Purpose : Deletion for Benficiary
Begin

insert into tbl_BeneficiaryServiceDelete

( intBenTempid, intIP, household_number, OLDEnrollmentNo, EnrollmentNo, NameofHousehold, FamilySize, Sex, Age, NameofSpouse, NoofAdult, 
                      NoofDependents, FamilyIncome, intStateid, intCountieid, intPayamid, intBomasid, Address, ContactNo, Email, SecondLineHome, IsDisability, intDisability, 
                      BiometricNo, NameofBank, AccountNo, WorkCode, LegalStatus, BenStatus, Created_by, Created_dt, Modified_by, Modified_dt, SOurceofIncome, RegYear, PortYear, 
                      PortPhase, PortedSheetName, BDropout, DropoutRemarks, GroupId, BeneficiaryType, Name1stAlternatePayee, Name2ndAlternatePayee, NumberMaleAdults, 
                      NumberFemaleAdults, NumberAdultsAgeAbove65, NumberAdultsAgeBetween45_64, NumberAdultsAgeBetween18_30, NumberAdultsAgeBetween30_45, 
                      NumberFemaleChildren, NumberMaleChildren, NumberChildrenAgeAbove5years, NumberChildrenAgeAbove12years, NumberChildrenAgeAbove18years, 
                      EthnicityTribeRgin, PhotoPath, PhotoName, NationalID, PCPortFlag, FirstAlternatePayeeGender, FirstAlternatePayeeAge, FirstAlternatePayeeNationID, 
                      FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath, SecondAlternatePayeeGender, SecondAlternatePayeeAge, 
                      SecondAlternateNationalID, SecondAlternatePayeeMobile, SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath, supervisor_id, member_number, 
                      female_dependants, male_dependants, latitude, longitude, id_url, TransferDate, TransferUpdateat, alternate_number, Secondalternate_number, API_Status, intBenid, 
                      Remarks

)

select intBenTempid, intIP, household_number, OLDEnrollmentNo, EnrollmentNo, NameofHousehold, FamilySize, Sex, Age, NameofSpouse, NoofAdult, 
                      NoofDependents, FamilyIncome, intStateid, intCountieid, intPayamid, intBomasid, Address, ContactNo, Email, SecondLineHome, IsDisability, intDisability, 
                      BiometricNo, NameofBank, AccountNo, WorkCode, LegalStatus, BenStatus, Created_by, Created_dt, Modified_by, Modified_dt, SOurceofIncome, RegYear, PortYear, 
                      PortPhase, PortedSheetName, BDropout, DropoutRemarks, GroupId, BeneficiaryType, Name1stAlternatePayee, Name2ndAlternatePayee, NumberMaleAdults, 
                      NumberFemaleAdults, NumberAdultsAgeAbove65, NumberAdultsAgeBetween45_64, NumberAdultsAgeBetween18_30, NumberAdultsAgeBetween30_45, 
                      NumberFemaleChildren, NumberMaleChildren, NumberChildrenAgeAbove5years, NumberChildrenAgeAbove12years, NumberChildrenAgeAbove18years, 
                      EthnicityTribeRgin, PhotoPath, PhotoName, NationalID, PCPortFlag, FirstAlternatePayeeGender, FirstAlternatePayeeAge, FirstAlternatePayeeNationID, 
                      FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath, SecondAlternatePayeeGender, SecondAlternatePayeeAge, 
                      SecondAlternateNationalID, SecondAlternatePayeeMobile, SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath, supervisor_id, member_number, 
                      female_dependants, male_dependants, latitude, longitude, id_url, TransferDate, TransferUpdateat, alternate_number, Secondalternate_number, API_Status, intBenid, 
                      @Remarks from tbl_BeneficiaryServiceDet where intBenTempid = @intBenid



					  Delete from tbl_BeneficiaryServiceDet where intBenTempid = @intBenid


select 1
return 1
End



