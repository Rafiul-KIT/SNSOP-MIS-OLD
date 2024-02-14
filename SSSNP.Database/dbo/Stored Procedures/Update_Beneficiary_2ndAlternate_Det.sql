
CREATE procedure [dbo].[Update_Beneficiary_2ndAlternate_Det]
  
@intBenid int ,
@Secondalternate_number	varchar(100),
@Name2ndAlternatePayee	varchar(100),
@SecondAlternatePayeeAge	varchar(100),
@SecondAlternateNationalID	varchar(100),
@SecondAlternatePayeeGender	varchar(1),
@SecondAlternatePayeeMobile	varchar(100),
@SecondAlternatepayeePhotoName	varchar(100)


 as begin  

if not exists(
select * from tbl_BeneficiaryServiceDet where (intBenTempid=@intBenid AND alternate_number=@Secondalternate_number)
)
 
 begin
		Update tbl_BeneficiaryServiceDet
		set 
		Secondalternate_number	=	@Secondalternate_number,
		Name2ndAlternatePayee	=	@Name2ndAlternatePayee,
		SecondAlternatePayeeAge	=	@SecondAlternatePayeeAge,
		SecondAlternateNationalID	=	@SecondAlternateNationalID,
		SecondAlternatePayeeGender	=	@SecondAlternatePayeeGender,
		SecondAlternatePayeeMobile	=	@SecondAlternatePayeeMobile,
		SecondAlternatepayeePhotoName	=	@SecondAlternatepayeePhotoName	
		Where intBenTempid = @intBenid
	END


if not exists(
select * from tbl_BeneficiaryDet where (intBenid=@intBenid AND alternate_number=@Secondalternate_number)
)

begin
		Declare @hhno as varchar(80)
		set @hhno=(select household_number from tbl_BeneficiaryServiceDet where  intBenTempid = @intBenid)
		if((select count(*) from tbl_BeneficiaryDet where household_number=@hhno)<>0)
		Begin
	Update tbl_BeneficiaryDet
		set 
		Secondalternate_number	=	@Secondalternate_number,
		Name2ndAlternatePayee	=	@Name2ndAlternatePayee,
		SecondAlternatePayeeAge	=	@SecondAlternatePayeeAge,
		SecondAlternateNationalID	=	@SecondAlternateNationalID,
		SecondAlternatePayeeGender	=	@SecondAlternatePayeeGender,
		SecondAlternatePayeeMobile	=	@SecondAlternatePayeeMobile,
		SecondAlternatepayeePhotoName	=	@SecondAlternatepayeePhotoName	
		Where household_number = @hhno
		End
	 END
end



