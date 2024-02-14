CREATE procedure [dbo].[DeleteBenficiariesBulkMail]
@Benid varchar(10)
as
begin
--Declare @Benid as int
--set @Benid=(select intBenid from tbl_BeneficiaryDet where EnrollmentNo='MUN3/05/010')

--select * from tr_BenfWorkDet where intBenid=@Benid
--select * from td_WageReqBenDet where intBenid=@Benid
--select * from tr_BenAttendanceDet where intBenid=@Benid

insert into tr_BenfWorkDetTrans
(
intBenfWorkid, intBenid, intWorkid, Created_by, Created_dt, Modified_by, Modifieid_dt, PortYear, PortPhase, PortedSheetName, 
                      Deleted_by, Deleted_dt

)
select

intBenfWorkid, intBenid, intWorkid, Created_by, Created_dt, Modified_by, Modifieid_dt, PortYear, PortPhase, PortedSheetName, 
                      '1000', getdate() from tr_BenfWorkDet  where intBenid=@Benid


					  insert into tr_BenAttendanceDetTrans
					  (
					   intAttendanceid, intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt, Modified_by, Modified_dt, 
                      AttendanceYear, PortYear, PortPhase, PortedSheetName, PaymentFlag, Deletedby, DeletedDt

					  )
					  select
					  
					    intAttendanceid, intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt, Modified_by, Modified_dt, 
                      AttendanceYear, PortYear, PortPhase, PortedSheetName, PaymentFlag, '1000', getdate()
					  
					   from tr_BenAttendanceDet  where intBenid=@Benid 


					   insert into tr_BenWagesPaymentTrans
					   (
					    intWageid, intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt, Modified_by, Modified_dt, Wagerate, WageRateUSD, 
                      PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, ExchangeRate, WageYear, SheetNo, PortYear, PortPhase, PortedSheetName, AttendanceDays, 
                      DeletedBy, DeletedDt

					   )

					   select  intWageid, intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt, Modified_by, Modified_dt, Wagerate, WageRateUSD, 
                      PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, ExchangeRate, WageYear, SheetNo, PortYear, PortPhase, PortedSheetName, AttendanceDays, 
                   '1000', getdate()

from tr_BenWagesPayment where  intBenid=@Benid 

insert into tbl_BeneficiaryDetTrans
(
 intBenid, intIP, OLDEnrollmentNo, EnrollmentNo, NameofHousehold, FamilySize, Sex, Age, NameofSpouse, NoofAdult, NoofDependents, FamilyIncome, intStateid, intCountieid, intPayamid, 
                         intBomasid, Address, ContactNo, Email, SecondLineHome, IsDisability, intDisability, BiometricNo, NameofBank, AccountNo, WorkCode, LegalStatus, Created_by, Created_dt, Modified_by, Modified_dt, SOurceofIncome, 
                         RegYear, PortYear, PortPhase, PortedSheetName, BDropout, DropoutRemarks, GroupId, BeneficiaryType, Name1stAlternatePayee, Name2ndAlternatePayee, NumberMaleAdults, NumberFemaleAdults, 
                         NumberAdultsAgeAbove65, NumberAdultsAgeBetween45_64, NumberAdultsAgeBetween18_30, NumberAdultsAgeBetween30_45, NumberFemaleChildren, NumberMaleChildren, NumberChildrenAgeAbove5years, 
                         NumberChildrenAgeAbove12years, NumberChildrenAgeAbove18years, EthnicityTribeRgin, PhotoPath, PhotoName, NationalID, PCPortFlag, household_number, FirstAlternatePayeeGender, FirstAlternatePayeeAge, 
                         FirstAlternatePayeeNationID, FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath, SecondAlternatePayeeGender, SecondAlternatePayeeAge, SecondAlternateNationalID, 
                         SecondAlternatePayeeMobile, SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath, supervisor_id, member_number, female_dependants, male_dependants, latitude, longitude, id_url, TransferDate, 
                         TransferUpdateat, alternate_number, Secondalternate_number, API_Status, DeletedBy, DeletedDt

)
select intBenid, intIP, OLDEnrollmentNo, EnrollmentNo, NameofHousehold, FamilySize, Sex, Age, NameofSpouse, NoofAdult, NoofDependents, FamilyIncome, intStateid, intCountieid, intPayamid, 
                         intBomasid, Address, ContactNo, Email, SecondLineHome, IsDisability, intDisability, BiometricNo, NameofBank, AccountNo, WorkCode, LegalStatus, Created_by, Created_dt, Modified_by, Modified_dt, SOurceofIncome, 
                         RegYear, PortYear, PortPhase, PortedSheetName, BDropout, DropoutRemarks, GroupId, BeneficiaryType, Name1stAlternatePayee, Name2ndAlternatePayee, NumberMaleAdults, NumberFemaleAdults, 
                         NumberAdultsAgeAbove65, NumberAdultsAgeBetween45_64, NumberAdultsAgeBetween18_30, NumberAdultsAgeBetween30_45, NumberFemaleChildren, NumberMaleChildren, NumberChildrenAgeAbove5years, 
                         NumberChildrenAgeAbove12years, NumberChildrenAgeAbove18years, EthnicityTribeRgin, PhotoPath, PhotoName, NationalID, PCPortFlag, household_number, FirstAlternatePayeeGender, FirstAlternatePayeeAge, 
                         FirstAlternatePayeeNationID, FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath, SecondAlternatePayeeGender, SecondAlternatePayeeAge, SecondAlternateNationalID, 
                         SecondAlternatePayeeMobile, SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath, supervisor_id, member_number, female_dependants, male_dependants, latitude, longitude, id_url, TransferDate, 
                         TransferUpdateat, alternate_number, Secondalternate_number, API_Status,'1000',getdate() from tbl_BeneficiaryDet where intBenid=@Benid

Delete from td_WageReqBenDet where intBenid=@Benid
Delete from tr_BenWagesPaymentNotPaid where intBenid=@Benid
Delete from tr_BenAttendanceDet where intBenid=@Benid
Delete from td_WageReqBenDet where intBenid=@Benid
Delete from td_WageReqBenPADet where intBenid=@Benid
Delete from tr_BenfWorkDet where intBenid=@Benid
Delete from tbl_BeneficiaryDet where intBenid=@Benid

--Update tbl_BeneficiaryDet set WorkCode=NULL where  intBenid=@Benid

select 1000
return 1000
end  