

CREATE procedure [dbo].[InsertAPIFailure_Status_alternate]
(
		@Household_Number nvarchar(300),
		@Failure_Message nvarchar(200),
		@TransferDate	varchar(100),
		@alternate_number nvarchar(300),
		@name varchar(180)
)
AS BEGIN

	Insert Into Beneficiary_API_Failure_Alternate
	(
		Household_Number,Failure_Message,Transfer_Date,PullDate,alternate_number,name
	)
	Values
	(
		@Household_Number,@Failure_Message,@TransferDate,getdate(),@alternate_number,@name
	)

END


