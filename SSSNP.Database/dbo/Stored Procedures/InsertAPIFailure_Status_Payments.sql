

CREATE procedure [dbo].[InsertAPIFailure_Status_Payments]
(
		@Household_Number nvarchar(300),
		@Failure_Message nvarchar(200),
		@TransferDate	varchar(100),
		@trans_id varchar(8),
		@work_id varchar(8),
        @uuid varchar(8)
)
AS BEGIN

	Insert Into Beneficiary_API_Failure_Payments
	(
		Household_Number,Failure_Message,Transfer_Date,PullDate,trans_id,work_id,uuid
	)
	Values
	(
		@Household_Number,@Failure_Message,@TransferDate,getdate(),@trans_id,@work_id,@uuid
	)

END


