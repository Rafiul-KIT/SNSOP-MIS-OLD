

CREATE procedure [dbo].[UpdateAlternateDetailsthree]
(
@intBenid varchar(8),

@Name3rdAlternatePayee varchar(100),

@ThirdAlternatePayeeGender	varchar(1)	,
@ThirdAlternatePayeeAge	varchar(8)	,
@ThirdAlternateNationalID	varchar(20)	,
@ThirdAlternatePayeeMobile	varchar(12)	,
@ThirdAlternatepayeePhotoName	varchar(200)	,
@ThirdAlternatePayeePhotoPath	varchar(200)	,
@Modified_by varchar(8),
@alternate_number varchar(340)
)
As
Begin

--Declare @oldalternatenumber as varchar(300)
--Declare @oldalternatename as varchar(100)
--set @oldalternatenumber=(select thirdalternate_number from tbl_BeneficiaryDet where intBenid=@intBenid)
--set @oldalternatename=(select Name3rdAlternatePayee from tbl_BeneficiaryDet where intBenid=@intBenid)
--if(isnull(@alternate_number,'0')  ='0')
--Begin
--if(isnull(@alternate_number,'0')<>isnull(@oldalternatenumber,'0'))
--Begin
--if(isnull(@oldalternatename,'0')<>isnull(@Name3rdAlternatePayee,'0'))
--Begin
insert into tbl_BenAlternateTransDet
(
intBenid, AlternateType, household_number, alternate_number, NameAlternatePayee, AlternatePayeeGender, AlternatePayeeAge, AlternatePayeeNationID, AlternatePayeeMobile, 
                         AlternatePayeePhotoName, AlternatePayeePhotoPath, Created_by, Created_dt

)
select intBenid,'Third Alternate',household_number,
thirdalternate_number,Name3rdAlternatePayee,ThirdAlternatePayeeGender,ThirdAlternatePayeeAge,ThirdAlternatePayeeNationID,ThirdAlternatePayeeMobile,
ThirdAlternatepayeePhotoName,ThirdAlternatePayeePhotoPath,@Modified_by,getdate()
 from tbl_BeneficiaryDet where intBenid=@intBenid


Update tbl_BeneficiaryDet set  Name3rdAlternatePayee=@Name3rdAlternatePayee ,

						 ThirdAlternatePayeeGender=@ThirdAlternatePayeeGender, ThirdAlternatePayeeAge=@ThirdAlternatePayeeAge, 
						 ThirdAlternatePayeeNationID=@ThirdAlternateNationalID, ThirdAlternatePayeeMobile=@ThirdAlternatePayeeMobile, 
                         ThirdAlternatepayeePhotoName=@ThirdAlternatepayeePhotoName, ThirdAlternatePayeePhotoPath=@ThirdAlternatePayeePhotoPath,
						 	ThirdAlternateUpdby=@Modified_by,ThirdAlternateUpdDate=getdate(),Modified_by=@Modified_by,Modified_dt=getdate(),thirdalternate_number=@alternate_number
where intBenid like @intBenid
--End
--End 
--End
delete from tbl_BenAlternateTransDet where alternate_number is null
select 1
return 1

ENd


