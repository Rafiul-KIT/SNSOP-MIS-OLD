

CREATE procedure [dbo].[InsertTransactionsDeletebenfBasicDetails]
(
 @Benid int,

 @DeletedBy int
)
As
Begin




  insert into tbl_BeneficiaryDetTrans
  (
   intBenid, intIP, OLDEnrollmentNo, EnrollmentNo, NameofHousehold, FamilySize, Sex, Age, NameofSpouse, NoofAdult, NoofDependents, FamilyIncome, intStateid, intCountieid, intPayamid, intBomasid, 
                         Address, ContactNo, Email, SecondLineHome, IsDisability, intDisability, BiometricNo, NameofBank, AccountNo, WorkCode, LegalStatus, Created_by, Created_dt, Modified_by, Modified_dt, SOurceofIncome, RegYear, PortYear, 
                         PortPhase, PortedSheetName, BDropout, DropoutRemarks, GroupId, BeneficiaryType, Name1stAlternatePayee, Name2ndAlternatePayee, NumberMaleAdults, NumberFemaleAdults, NumberAdultsAgeAbove65, 
                         NumberAdultsAgeBetween45_64, NumberAdultsAgeBetween18_30, NumberAdultsAgeBetween30_45, NumberFemaleChildren, NumberMaleChildren, NumberChildrenAgeAbove5years, NumberChildrenAgeAbove12years, 
                         NumberChildrenAgeAbove18years, EthnicityTribeRgin, PhotoPath, PhotoName, NationalID, PCPortFlag, household_number, FirstAlternatePayeeGender, FirstAlternatePayeeAge, FirstAlternatePayeeNationID, 
                         FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath, SecondAlternatePayeeGender, SecondAlternatePayeeAge, SecondAlternateNationalID, SecondAlternatePayeeMobile, 
                         SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath, supervisor_id, member_number, female_dependants, male_dependants, latitude, longitude, id_url, TransferDate, TransferUpdateat, alternate_number, 
                         Secondalternate_number, API_Status, DeletedBy, DeletedDt, DeletedRemarks

  )
  select
   intBenid, intIP, OLDEnrollmentNo, EnrollmentNo, NameofHousehold, FamilySize, Sex, Age, NameofSpouse, NoofAdult, NoofDependents, FamilyIncome, intStateid, intCountieid, intPayamid, intBomasid, 
                         Address, ContactNo, Email, SecondLineHome, IsDisability, intDisability, BiometricNo, NameofBank, AccountNo, WorkCode, LegalStatus, Created_by, Created_dt, Modified_by, Modified_dt, SOurceofIncome, RegYear, PortYear, 
                         PortPhase, PortedSheetName, BDropout, DropoutRemarks, GroupId, BeneficiaryType, Name1stAlternatePayee, Name2ndAlternatePayee, NumberMaleAdults, NumberFemaleAdults, NumberAdultsAgeAbove65, 
                         NumberAdultsAgeBetween45_64, NumberAdultsAgeBetween18_30, NumberAdultsAgeBetween30_45, NumberFemaleChildren, NumberMaleChildren, NumberChildrenAgeAbove5years, NumberChildrenAgeAbove12years, 
                         NumberChildrenAgeAbove18years, EthnicityTribeRgin, PhotoPath, PhotoName, NationalID, PCPortFlag, household_number, FirstAlternatePayeeGender, FirstAlternatePayeeAge, FirstAlternatePayeeNationID, 
                         FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath, SecondAlternatePayeeGender, SecondAlternatePayeeAge, SecondAlternateNationalID, SecondAlternatePayeeMobile, 
                         SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath, supervisor_id, member_number, female_dependants, male_dependants, latitude, longitude, id_url, TransferDate, TransferUpdateat, alternate_number, 
                         Secondalternate_number, API_Status, @DeletedBy, getdate(),'Deleted on' + Convert(varchar, (getdate()))  from 
  tbl_BeneficiaryDet  where intBenid = @Benid

insert into tbl_BeneficiaryServiceDelete
(
 intBenTempid, intIP, household_number, OLDEnrollmentNo, EnrollmentNo, NameofHousehold, FamilySize, Sex, Age, NameofSpouse, NoofAdult, NoofDependents, FamilyIncome, intStateid, intCountieid, 
                         intPayamid, intBomasid, Address, ContactNo, Email, SecondLineHome, IsDisability, intDisability, BiometricNo, NameofBank, AccountNo, WorkCode, LegalStatus, BenStatus, Created_by, Created_dt, Modified_by, Modified_dt, 
                         SOurceofIncome, RegYear, PortYear, PortPhase, PortedSheetName, BDropout, DropoutRemarks, GroupId, BeneficiaryType, Name1stAlternatePayee, Name2ndAlternatePayee, NumberMaleAdults, NumberFemaleAdults, 
                         NumberAdultsAgeAbove65, NumberAdultsAgeBetween45_64, NumberAdultsAgeBetween18_30, NumberAdultsAgeBetween30_45, NumberFemaleChildren, NumberMaleChildren, NumberChildrenAgeAbove5years, 
                         NumberChildrenAgeAbove12years, NumberChildrenAgeAbove18years, EthnicityTribeRgin, PhotoPath, PhotoName, NationalID, PCPortFlag, FirstAlternatePayeeGender, FirstAlternatePayeeAge, FirstAlternatePayeeNationID, 
                         FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath, SecondAlternatePayeeGender, SecondAlternatePayeeAge, SecondAlternateNationalID, SecondAlternatePayeeMobile, 
                         SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath, supervisor_id, member_number, female_dependants, male_dependants, latitude, longitude, id_url, TransferDate, TransferUpdateat, alternate_number, 
                         Secondalternate_number, API_Status, intBenid, Remarks, InputDate
)
select  intBenTempid, intIP, household_number, OLDEnrollmentNo, EnrollmentNo, NameofHousehold, FamilySize, Sex, Age, NameofSpouse, NoofAdult, NoofDependents, FamilyIncome, intStateid, intCountieid, 
                         intPayamid, intBomasid, Address, ContactNo, Email, SecondLineHome, IsDisability, intDisability, BiometricNo, NameofBank, AccountNo, WorkCode, LegalStatus, BenStatus, Created_by, Created_dt, Modified_by, Modified_dt, 
                         SOurceofIncome, RegYear, PortYear, PortPhase, PortedSheetName, BDropout, DropoutRemarks, GroupId, BeneficiaryType, Name1stAlternatePayee, Name2ndAlternatePayee, NumberMaleAdults, NumberFemaleAdults, 
                         NumberAdultsAgeAbove65, NumberAdultsAgeBetween45_64, NumberAdultsAgeBetween18_30, NumberAdultsAgeBetween30_45, NumberFemaleChildren, NumberMaleChildren, NumberChildrenAgeAbove5years, 
                         NumberChildrenAgeAbove12years, NumberChildrenAgeAbove18years, EthnicityTribeRgin, PhotoPath, PhotoName, NationalID, PCPortFlag, FirstAlternatePayeeGender, FirstAlternatePayeeAge, FirstAlternatePayeeNationID, 
                         FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath, SecondAlternatePayeeGender, SecondAlternatePayeeAge, SecondAlternateNationalID, SecondAlternatePayeeMobile, 
                         SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath, supervisor_id, member_number, female_dependants, male_dependants, latitude, longitude, id_url, TransferDate, TransferUpdateat, alternate_number, 
                         Secondalternate_number, API_Status, intBenid, 'Deleted on' + Convert(varchar, (getdate())) , getdate() from tbl_BeneficiaryServiceDet where household_number in
  (
  select tbl_BeneficiaryDet.household_number
 from tbl_BeneficiaryDet  where intBenid = @Benid
  )

  


update tbl_BeneficiaryServiceDet set BenStatus='3'
  where household_number in
  (
  select tbl_BeneficiaryDet.household_number
 from tbl_BeneficiaryDet  
 where intBenid = @Benid
  )

  

   Delete
 from tbl_BeneficiaryDet where intBenid = @Benid

  select 1000
  return 1000
End


