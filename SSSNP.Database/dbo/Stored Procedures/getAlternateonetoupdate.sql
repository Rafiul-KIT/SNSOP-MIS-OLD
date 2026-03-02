
CREATE procedure [dbo].[getAlternateonetoupdate]
As
Begin
select  intBenTempid, alternate_number,FirstAlternatePayeePhotoName,household_number from tbl_BeneficiaryServiceDet where FirstAlternatePayeePhotoPath is null and FirstAlternatePayeePhotoName is not null
select  intBenTempid, Secondalternate_number,SecondAlternatepayeePhotoName,household_number from tbl_BeneficiaryServiceDet where SecondAlternatePayeePhotoPath is null and SecondAlternatePayeePhotoName is not null

End
