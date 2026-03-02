

CREATE procedure [dbo].[InsertAPIFailure_StatusIPidNew]
(
		@Household_Number nvarchar(500),
		@Failure_Message nvarchar(200),
		@TransferDate	varchar(100),
		@IPid varchar(8),
		@Bomaid varchar(8),
		@NameofHousehold varchar(180)
)
AS BEGIN

	Insert Into Beneficiary_API_Failure_Beneficiary
	(
		Household_Number,Failure_Message,Transfer_Date,Ipid,intBomaid,PullDate,NameofHousehold
	)
	Values
	(
		@Household_Number,@Failure_Message,@TransferDate,@IPid,@Bomaid,getdate(),@NameofHousehold
	)

END
