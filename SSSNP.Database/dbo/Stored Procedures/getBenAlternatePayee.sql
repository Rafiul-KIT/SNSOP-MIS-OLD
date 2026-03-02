

CREATE procedure [dbo].[getBenAlternatePayee]
AS
Begin

select  intBenid,EnrollmentNo, household_number, alternate_number as member_number,Name1stAlternatePayee as Name,FirstAlternatePayeeGender as Gender,
FirstAlternatePayeeAge as Age,FirstAlternatePayeeNationID as NationalID,FirstAlternatePayeeMobile as Mobile,FirstAlternatePayeePhotoPath+'/' + FirstAlternatePayeePhotoName as PhotoURL from tbl_BeneficiaryDet  where (Name1stAlternatePayee is not null and Name1stAlternatePayee<>'0')
union
select  intBenid,EnrollmentNo, household_number,Secondalternate_number as member_number,Name2ndAlternatePayee as Name,SecondAlternatePayeeGender as Gender,
SecondAlternatePayeeAge as Age,SecondAlternateNationalID as NationalID,SecondAlternatePayeeMobile as Mobile,SecondAlternatepayeePhotoPath+'/' + SecondAlternatepayeePhotoName as PhotoURL from tbl_BeneficiaryDet  where (Name2ndAlternatePayee is not null and Name2ndAlternatePayee<>'0')
End
