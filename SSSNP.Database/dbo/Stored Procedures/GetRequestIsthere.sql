
CREATE procedure [dbo].[GetRequestIsthere](@WorkCode varchar(100))
as
select * from td_WorkWageRequest where ReqNo=@WorkCode 


