
CREATE procedure [dbo].[UpdateAlternateDetails]
(
@intBenid varchar(8),
@Name1stAlternatePayee varchar(100),
@Name2ndAlternatePayee varchar(100),
@FirstAlternatePayeeGender	varchar(1)	,
@FirstAlternatePayeeAge	varchar(8)	,
@FirstAlternatePayeeNationID	varchar(20)	,
@FirstAlternatePayeeMobile	varchar(12)	,
@FirstAlternatePayeePhotoName	varchar(200)	,
@FirstAlternatePayeePhotoPath	varchar(200)	,
@SecondAlternatePayeeGender	varchar(1)	,
@SecondAlternatePayeeAge	varchar(8)	,
@SecondAlternateNationalID	varchar(20)	,
@SecondAlternatePayeeMobile	varchar(12)	,
@SecondAlternatepayeePhotoName	varchar(200)	,
@SecondAlternatePayeePhotoPath	varchar(200)	
)
As
Begin

Update tbl_BeneficiaryDet set Name1stAlternatePayee=@Name1stAlternatePayee, Name2ndAlternatePayee=@Name2ndAlternatePayee ,
FirstAlternatePayeeGender=@FirstAlternatePayeeGender, FirstAlternatePayeeAge=@FirstAlternatePayeeAge, FirstAlternatePayeeNationID=@FirstAlternatePayeeNationID, 
                         FirstAlternatePayeeMobile=@FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName=@FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath=@FirstAlternatePayeePhotoPath, 
						 SecondAlternatePayeeGender=@SecondAlternatePayeeGender, SecondAlternatePayeeAge=@SecondAlternatePayeeAge, 
						 SecondAlternateNationalID=@SecondAlternateNationalID, SecondAlternatePayeeMobile=@SecondAlternatePayeeMobile, 
                         SecondAlternatepayeePhotoName=@SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath=@SecondAlternatePayeePhotoPath
where intBenid like @intBenid

select 1
return 1

ENd

