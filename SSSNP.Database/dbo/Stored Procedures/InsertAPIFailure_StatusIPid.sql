
CREATE procedure [dbo].[InsertAPIFailure_StatusIPid]
(
		@Household_Number nvarchar(500),
		@Failure_Message nvarchar(200),
		@TransferDate	varchar(100),
		@IPid varchar(8)
)
AS BEGIN

	Insert Into Beneficiary_API_Failure
	(
		Household_Number,Failure_Message,Transfer_Date,Ipid
	)
	Values
	(
		@Household_Number,@Failure_Message,@TransferDate,@IPid
	)

END