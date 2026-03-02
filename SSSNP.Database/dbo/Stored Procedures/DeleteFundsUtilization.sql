CREATE Procedure  [dbo].[DeleteFundsUtilization]    
(@intWorkid int )  
As
begin      
delete from tbl_FundsUtilization where intWorkid=@intWorkid       

end



