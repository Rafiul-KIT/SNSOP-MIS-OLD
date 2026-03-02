

CREATE procedure [dbo].[InsertAPIFailure_Status_attendance]
(
		@Household_Number nvarchar(300),
		@Failure_Message nvarchar(200),
		@TransferDate	varchar(100),
		@AttedanceDate datetime,
		@work_id varchar(8)
)
AS BEGIN

	Insert Into Beneficiary_API_Failure_Attedance
	(
		Household_Number,Failure_Message,Transfer_Date,PullDate,AttedanceDate,work_id
	)
	Values
	(
		@Household_Number,@Failure_Message,@TransferDate,getdate(),@AttedanceDate,@work_id
	)

END


