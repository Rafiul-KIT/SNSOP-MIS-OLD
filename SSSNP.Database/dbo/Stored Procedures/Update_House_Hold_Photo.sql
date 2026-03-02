
CREATE procedure [dbo].[Update_House_Hold_Photo]
  
	@intBenid	int,
	@PhotoPath varchar(100),
	@column_name varchar(200),
	@photoname varchar(120)


  
as begin  
   if(@column_name='PhotoPath')
   BEGIN
	Update tbl_BeneficiaryServiceDet
	Set PhotoPath = @PhotoPath,PhotoName=@photoname Where intBenTempid = @intBenid
  END
  if(@column_name='FirstAlternatePayeePhotoPath')
   BEGIN
	Update tbl_BeneficiaryServiceDet
	Set FirstAlternatePayeePhotoPath = @PhotoPath,FirstAlternatePayeePhotoName=@photoname Where intBenTempid = @intBenid
  END
  if(@column_name='SecondAlternatePayeePhotoPath')
   BEGIN
	Update tbl_BeneficiaryServiceDet
	Set SecondAlternatePayeePhotoPath = @PhotoPath,SecondAlternatepayeePhotoName=@photoname Where intBenTempid = @intBenid
  END
end



