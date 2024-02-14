

CREATE procedure [dbo].[sp_getWRCreator]        --exec sp_getusersdata 'ip'
@intWorkWageId varchar(50)
as
begin
select * from tbl_Users 
	where intUserId = (
						select created_by 
							from td_WorkWageRequest 
							where intWagePaymentReqid=@intWorkWageId 
					)
end