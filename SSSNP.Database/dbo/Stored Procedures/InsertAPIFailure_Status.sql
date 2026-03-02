
CREATE procedure [dbo].[InsertAPIFailure_Status]
(
		@Household_Number nvarchar(500),
		@Failure_Message nvarchar(200),
		@TransferDate	varchar(100)
)
AS BEGIN

	Insert Into Beneficiary_API_Failure
	(
		Household_Number,Failure_Message,Transfer_Date
	)
	Values
	(
		@Household_Number,@Failure_Message,@TransferDate
	)

END

