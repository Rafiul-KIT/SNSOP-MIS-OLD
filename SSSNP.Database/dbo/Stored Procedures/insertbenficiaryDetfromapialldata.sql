CREATE procedure [dbo].[insertbenficiaryDetfromapialldata]
  (
	@id	varchar(15),
@supervisor_id	varchar(8),
@partner_code	varchar(8),
@household_number	varchar(300),
@beneficiary_type	varchar(20),
@group_number	varchar(10),
@member_number	varchar(10),
@household_size	varchar(10),
@household_name	varchar(180),
@age	varchar(8),
@marital_status	varchar(15),
@spouse_name	varchar(180),
@id_number	varchar(20),
@phone_number	varchar(20),
@gender	varchar(12),
@female_dependants	varchar(8),
@male_dependants	varchar(8),
@zero_five	varchar(8),
@six_eighteen	varchar(8),
@nineteen_forty_five	varchar(8),
@forty_six_sixty_five	varchar(8),
@sixty_six	varchar(8),
@income_household	varchar(15),
@average_household	varchar(15),
@state	int,
@county	int,
@payam	int,
@boma	int,
@latitude	varchar(25),
@longitude	varchar(25),
@legal_status	varchar(30),
@status	varchar(8),
@created_at	datetime,
@updated_at	datetime,
@stored_at	datetime,
@photo_url	varchar(450),

	
	@ReturnVal int output

	)
  
as begin  
  
  insert into tblTotalBeneficiariesDet
  (
   id, supervisor_id, partner_code, household_number, beneficiary_type, group_number, member_number, household_size, household_name, age, marital_status, spouse_name, id_number, phone_number, 
                         gender, female_dependants, male_dependants, zero_five, six_eighteen, nineteen_forty_five, forty_six_sixty_five, sixty_six, income_household, average_household, state, county, payam, boma, latitude, longitude, 
                         legal_status, status, created_at, updated_at, stored_at, photo_url, created_by, created_dt

  )
  values
  (

   @id, @supervisor_id, @partner_code, @household_number, @beneficiary_type, @group_number, @member_number, @household_size, @household_name, @age, @marital_status, @spouse_name,
    @id_number, @phone_number, 
                         @gender, @female_dependants, @male_dependants, @zero_five, @six_eighteen, @nineteen_forty_five, @forty_six_sixty_five, @sixty_six, @income_household, @average_household, @state, @county, @payam, @boma, @latitude, @longitude, 
                         @legal_status, @status, @created_at, @updated_at, @stored_at, @photo_url, '1000', getdate()

  )





   Declare @ids  as int          
	set @ids=@@IDENTITY
	
   set @ReturnVal = @ids

 end


