
CREATE procedure [dbo].[Update_House_Hold_PhotoHHNew]
  
	@intBenid	int,
	@PhotoPath varchar(100),
	@column_name varchar(200),
	@photoname varchar(120),
	@household_name varchar(300)


  
as begin  
   if(@column_name='PhotoPath')
   BEGIN
	Update tbl_BeneficiaryServiceDet
	Set PhotoPath = @PhotoPath,PhotoName=@photoname,householdnameupdate='P' Where intBenTempid = @intBenid
	Update tbl_BeneficiaryDet set PhotoPath = @PhotoPath,PhotoName=@photoname Where household_number like @household_name
  END
  
end