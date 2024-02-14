CREATE procedure [dbo].[getCheckingWagesApplyrestriction]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(8)
)  
as  
begin  
  select * from td_WorkWageRequest where intWorkid like @intWorkActivityId and Convert(varchar(11),ReqFromDate,103)= Convert(varchar(11),@FromDate,103) and  Convert(varchar(11),ReqToDate,103)= Convert(varchar(11),@ToDate,103)
end


