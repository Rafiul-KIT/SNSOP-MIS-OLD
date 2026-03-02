
CREATE procedure [dbo].[UpdateAlternateSingels]
As
Begin

update a set a.Name1stAlternatePayee=b.Name1stAlternatePayee,a.Name2ndAlternatePayee=b.Name2ndAlternatePayee,
a.FirstAlternatePayeeGender=b.FirstAlternatePayeeGender,
a.FirstAlternatePayeeAge=b.FirstAlternatePayeeAge,a.FirstAlternatePayeeNationID=b.FirstAlternatePayeeNationID,a.FirstAlternatePayeeMobile=b.FirstAlternatePayeeMobile,
a.FirstAlternatePayeePhotoName=b.FirstAlternatePayeePhotoName,
a.FirstAlternatePayeePhotoPath=b.FirstAlternatePayeePhotoPath,
a.SecondAlternatePayeeGender=b.SecondAlternatePayeeGender,a.SecondAlternatePayeeAge=b.SecondAlternatePayeeAge,
a.SecondAlternateNationalID=b.SecondAlternateNationalID,a.SecondAlternatePayeeMobile=b.SecondAlternatePayeeMobile,
a.SecondAlternatepayeePhotoName=b.SecondAlternatepayeePhotoName,a.SecondAlternatePayeePhotoPath=b.SecondAlternatePayeePhotoPath,
a.alternate_number=b.alternate_number,a.Secondalternate_number=b.Secondalternate_number
from tbl_BeneficiaryDet as a inner join tbl_BeneficiaryServiceDet as b on b.household_number=a.household_number
where a.Name1stAlternatePayee is null or a.Name2ndAlternatePayee is null



update tbl_BeneficiaryDet set Name2ndAlternatePayee=NULL,
SecondAlternatePayeeGender=NULL,SecondAlternatePayeeAge=NULL,SecondAlternateNationalID=NULL,SecondAlternatePayeeMobile=NULL,
SecondAlternatepayeePhotoName=NULL,SecondAlternatePayeePhotoPath=NULL,Secondalternate_number=NULL
where alternate_number=Secondalternate_number

update tbl_BeneficiaryServiceDet set Name2ndAlternatePayee=NULL,
SecondAlternatePayeeGender=NULL,SecondAlternatePayeeAge=NULL,SecondAlternateNationalID=NULL,SecondAlternatePayeeMobile=NULL,
SecondAlternatepayeePhotoName=NULL,SecondAlternatePayeePhotoPath=NULL,Secondalternate_number=NULL
where alternate_number=Secondalternate_number




select 1000
return 1000
End
