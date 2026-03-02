
CREATE procedure [dbo].[Update_Beneficiary_Alternate_Det]
  
@intBenid int ,	 
@alternate_number	varchar(100),
@Name1stAlternatePayee	varchar(100),
@FirstAlternatePayeeAge	varchar(100),
@FirstAlternatePayeeNationID	varchar(100),
@FirstAlternatePayeeGender	varchar(1),
@FirstAlternatePayeeMobile	varchar(100),
@FirstAlternatePayeePhotoName	varchar(100)


  
as begin  
   
		Update tbl_BeneficiaryServiceDet
		set Alternate_number	=	@alternate_number,
		Name1stAlternatePayee	=	@Name1stAlternatePayee,
		FirstAlternatePayeeAge	=	@FirstAlternatePayeeAge,
		FirstAlternatePayeeNationID	=	@FirstAlternatePayeeNationID,
		FirstAlternatePayeeGender	=	@FirstAlternatePayeeGender,
		FirstAlternatePayeeMobile	=	@FirstAlternatePayeeMobile,
		FirstAlternatePayeePhotoName	=	@FirstAlternatePayeePhotoName
		Where intBenTempid = @intBenid

		Declare @hhno as varchar(80)
		set @hhno=(select household_number from tbl_BeneficiaryServiceDet where  intBenTempid = @intBenid)
		if((select count(*) from tbl_BeneficiaryDet where household_number=@hhno)<>0)
		Begin
		Update tbl_BeneficiaryDet
		set Alternate_number	=	@alternate_number,
		Name1stAlternatePayee	=	@Name1stAlternatePayee,
		FirstAlternatePayeeAge	=	@FirstAlternatePayeeAge,
		FirstAlternatePayeeNationID	=	@FirstAlternatePayeeNationID,
		FirstAlternatePayeeGender	=	@FirstAlternatePayeeGender,
		FirstAlternatePayeeMobile	=	@FirstAlternatePayeeMobile,
		FirstAlternatePayeePhotoName	=	@FirstAlternatePayeePhotoName
		Where household_number = @hhno
		End
	
	
	 
end



