

CREATE procedure [dbo].[UpdateAlternateDetailsLatestwithnumbers]
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
@SecondAlternatePayeePhotoPath	varchar(200)	,
@household_number varchar(200),
@alternate_number varchar(200),
@Secondalternate_number varchar(200)
)
As
Begin

Update tbl_BeneficiaryDet set Name1stAlternatePayee=@Name1stAlternatePayee, Name2ndAlternatePayee=@Name2ndAlternatePayee ,
FirstAlternatePayeeGender=@FirstAlternatePayeeGender, FirstAlternatePayeeAge=@FirstAlternatePayeeAge, FirstAlternatePayeeNationID=@FirstAlternatePayeeNationID, 
                         FirstAlternatePayeeMobile=@FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName=@FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath=@FirstAlternatePayeePhotoPath, 
						 SecondAlternatePayeeGender=@SecondAlternatePayeeGender, SecondAlternatePayeeAge=@SecondAlternatePayeeAge, 
						 SecondAlternateNationalID=@SecondAlternateNationalID, SecondAlternatePayeeMobile=@SecondAlternatePayeeMobile, 
                         SecondAlternatepayeePhotoName=@SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath=@SecondAlternatePayeePhotoPath,
						 household_number=@household_number,alternate_number=@alternate_number,Secondalternate_number=@Secondalternate_number
where intBenid like @intBenid

select 1
return 1

ENd


