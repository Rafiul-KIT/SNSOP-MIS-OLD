

CREATE procedure [dbo].[getBenAlternatePayeePreEnrolled]
AS
Begin

select  intBenTempid,EnrollmentNo, household_number, alternate_number as member_number,Name1stAlternatePayee as Name,FirstAlternatePayeeGender as Gender,
FirstAlternatePayeeAge as Age,FirstAlternatePayeeNationID as NationalID,FirstAlternatePayeeMobile as Mobile,Replace(FirstAlternatePayeePhotoPath,'','http://wwww.snsdpmis.com')+'/' + FirstAlternatePayeePhotoName as PhotoURL from tbl_BeneficiaryServiceDet  where (Name1stAlternatePayee is not null and Name1stAlternatePayee<>'0')
union
select  intBenTempid,EnrollmentNo, household_number,Secondalternate_number as member_number,Name2ndAlternatePayee as Name,SecondAlternatePayeeGender as Gender,
SecondAlternatePayeeAge as Age,SecondAlternateNationalID as NationalID,SecondAlternatePayeeMobile as Mobile,Replace(SecondAlternatepayeePhotoPath,'','http://wwww.snsdpmis.com')+'/' + SecondAlternatepayeePhotoName as PhotoURL from tbl_BeneficiaryServiceDet  where (Name2ndAlternatePayee is not null and Name2ndAlternatePayee<>'0')
End


