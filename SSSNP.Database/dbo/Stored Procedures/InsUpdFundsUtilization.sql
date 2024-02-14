CREATE Procedure  [dbo].[InsUpdFundsUtilization]    
(        
        
@intWorkid int,        
@FundsUtilizationDate date,        
@FundsUtilizationFor varchar(100),        
@Remarks varchar(150),
@Amount float,        
@Created_by int     
       
)        
as begin        
if((select COUNT(*) from tbl_FundsUtilization where intWorkid=@intWorkid and FundsUtilizationFor=@FundsUtilizationFor)>0)        
begin        
        
update tbl_FundsUtilization set   FundsUtilizationDate=@FundsUtilizationDate, FundsUtilizationFor=@FundsUtilizationFor, Remarks=@Remarks,Amount=@Amount, Modified_by=@Created_by, Modied_dt=GETDATE()   
   where intWorkid=@intWorkid  and  FundsUtilizationFor=@FundsUtilizationFor    
  
 declare  @intFundsUtilization int
 set @intFundsUtilization=(select intFundsUtilization from tbl_FundsUtilization where intWorkid=@intWorkid and FundsUtilizationFor=@FundsUtilizationFor  )       
select @intFundsUtilization       
return @intFundsUtilization      
        
end        
else        
begin        
insert into tbl_FundsUtilization (intWorkid, FundsUtilizationDate, FundsUtilizationFor, Remarks,Amount,  Created_by, Created_dt)        
values(@intWorkid, @FundsUtilizationDate, @FundsUtilizationFor, @Remarks,@Amount, @Created_by, GETDATE())        
          
select @@IDENTITY        
return @@IDENTITY        
end        
end



