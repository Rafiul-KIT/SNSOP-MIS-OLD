

CREATE procedure [dbo].[UpdateAlternateDetailstwo]
(
@intBenid varchar(8),

@Name2ndAlternatePayee varchar(100),

@SecondAlternatePayeeGender	varchar(1)	,
@SecondAlternatePayeeAge	varchar(8)	,
@SecondAlternateNationalID	varchar(20)	,
@SecondAlternatePayeeMobile	varchar(12)	,
@SecondAlternatepayeePhotoName	varchar(200)	,
@SecondAlternatePayeePhotoPath	varchar(200)	,
@Modified_by varchar(8),
@alternate_number varchar(340)
)
As
Begin

--Declare @oldalternatenumber as varchar(300)
--Declare @oldalternatename as varchar(100)
--set @oldalternatenumber=(select Secondalternate_number from tbl_BeneficiaryDet where intBenid=@intBenid)
--set @oldalternatename=(select Name2ndAlternatePayee from tbl_BeneficiaryDet where intBenid=@intBenid)
--if(@alternate_number  is not null)
--Begin
--if(@alternate_number<>@oldalternatenumber)
--Begin
--if(@oldalternatename<>@Name2ndAlternatePayee)
--Begin
insert into tbl_BenAlternateTransDet
(
intBenid, AlternateType, household_number, alternate_number, NameAlternatePayee, AlternatePayeeGender, AlternatePayeeAge, AlternatePayeeNationID, AlternatePayeeMobile, 
                         AlternatePayeePhotoName, AlternatePayeePhotoPath, Created_by, Created_dt

)
select intBenid,'Second Alternate',household_number,
Secondalternate_number,Name2ndAlternatePayee,SecondAlternatePayeeGender,SecondAlternatePayeeAge,SecondAlternateNationalID,SecondAlternatePayeeMobile,
SecondAlternatepayeePhotoName,SecondAlternatePayeePhotoPath,@Modified_by,getdate()
 from tbl_BeneficiaryDet where intBenid=@intBenid


Update tbl_BeneficiaryDet set  Name2ndAlternatePayee=@Name2ndAlternatePayee ,

						 SecondAlternatePayeeGender=@SecondAlternatePayeeGender, SecondAlternatePayeeAge=@SecondAlternatePayeeAge, 
						 SecondAlternateNationalID=@SecondAlternateNationalID, SecondAlternatePayeeMobile=@SecondAlternatePayeeMobile, 
                         SecondAlternatepayeePhotoName=@SecondAlternatepayeePhotoName, SecondAlternatePayeePhotoPath=@SecondAlternatePayeePhotoPath,
						 	SecondAlternateUpdby=@Modified_by,SecondAlternateUpdDate=getdate(),Modified_by=@Modified_by,Modified_dt=getdate(),Secondalternate_number=@alternate_number
where intBenid like @intBenid
delete from tbl_BenAlternateTransDet where alternate_number is null
--End
--End 
--End
select 1
return 1

ENd


