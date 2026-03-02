
CREATE procedure [dbo].[InsertAlternate]
  
@household_number varchar(100),
@Secondalternate_number	varchar(100),
@Name2ndAlternatePayee	varchar(100),
@SecondAlternatePayeeAge	varchar(100),
@SecondAlternateNationalID	varchar(100),
@SecondAlternatePayeeGender	varchar(1),
@SecondAlternatePayeeMobile	varchar(100),
@SecondAlternatepayeePhotoName	varchar(100),
@id	varchar(8),
@created_dt	datetime,
@supervisor_id	varchar(8)


  
as begin  
   insert into tbl_BenAlternateDump
   (
   id, supervisor_id, household_number, alternate_number, alternate_name, age, id_number, phone_number, gender, status, created_at,  stored_at, photo_url

   )
   values
   (
   @id,@supervisor_id,@household_number,@Secondalternate_number,@Name2ndAlternatePayee,@SecondAlternatePayeeAge,@SecondAlternateNationalID,@SecondAlternatePayeeMobile
   ,@SecondAlternatePayeeGender,'1',@created_dt,getdate(),@SecondAlternatepayeePhotoName

   )

   select 1
   return 1
	
	 
end



