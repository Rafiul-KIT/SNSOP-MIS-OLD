CREATE Procedure  [dbo].[sp_deletewages]                          
  (                          
                          
@ids varchar(50) ,        
@Created_dt datetime ,
@intWorkid varchar(8)                        
                         
                          
 )                          
AS                          
begin                          
set nocount on                          
                          
delete from tr_BenWagesPayment where intBenid like @ids and  intWorkid like @intWorkid   and
 convert(varchar,cast(PaidDate as datetime),103) LIKE convert(varchar,cast(@Created_dt as datetime),103)
  --PaidDate like @Created_dt                         
                          
                          
                          
                          
end




