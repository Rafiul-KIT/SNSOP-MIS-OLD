

CREATE procedure [dbo].[UpdateAlternateDetailsone]
(
@intBenid varchar(8),
@Name1stAlternatePayee varchar(100),
@FirstAlternatePayeeGender	varchar(1)	,
@FirstAlternatePayeeAge	varchar(8)	,
@FirstAlternatePayeeNationID	varchar(20)	,
@FirstAlternatePayeeMobile	varchar(12)	,
@FirstAlternatePayeePhotoName	varchar(200)	,
@FirstAlternatePayeePhotoPath	varchar(200)	,
@Modified_by varchar(8),
@alternate_number varchar(340)
)
As
Begin

--Declare @oldalternatenumber as varchar(300)
--Declare @oldalternatename as varchar(100)
--set @oldalternatenumber=(select alternate_number from tbl_BeneficiaryDet where intBenid=@intBenid)
--set @oldalternatename=(select Name1stAlternatePayee from tbl_BeneficiaryDet where intBenid=@intBenid)
--if(@alternate_number  is not null)
--Begin
--if(@alternate_number<>@oldalternatenumber)
--Begin
--if(@oldalternatename<>@Name1stAlternatePayee)
--Begin
insert into tbl_BenAlternateTransDet
(
intBenid, AlternateType, household_number, alternate_number, NameAlternatePayee, AlternatePayeeGender, AlternatePayeeAge, AlternatePayeeNationID, AlternatePayeeMobile, 
                         AlternatePayeePhotoName, AlternatePayeePhotoPath, Created_by, Created_dt

)
select intBenid,'First Alternate',household_number,
alternate_number,Name1stAlternatePayee,FirstAlternatePayeeGender,FirstAlternatePayeeAge,FirstAlternatePayeeNationID,FirstAlternatePayeeMobile,
FirstAlternatePayeePhotoName,FirstAlternatePayeePhotoPath,@Modified_by,getdate()
 from tbl_BeneficiaryDet where intBenid=@intBenid

Update tbl_BeneficiaryDet set Name1stAlternatePayee=@Name1stAlternatePayee,
FirstAlternatePayeeGender=@FirstAlternatePayeeGender, FirstAlternatePayeeAge=@FirstAlternatePayeeAge, FirstAlternatePayeeNationID=@FirstAlternatePayeeNationID, 
                         FirstAlternatePayeeMobile=@FirstAlternatePayeeMobile, FirstAlternatePayeePhotoName=@FirstAlternatePayeePhotoName, FirstAlternatePayeePhotoPath=@FirstAlternatePayeePhotoPath, 
						FirstAlternateUpdby=@Modified_by,FirstAlternateUpdDate=getdate(),Modified_by=@Modified_by,Modified_dt=getdate(),alternate_number=@alternate_number
where intBenid like @intBenid
delete from tbl_BenAlternateTransDet where alternate_number is null
--End
--End
--End
select 1
return 1


ENd


