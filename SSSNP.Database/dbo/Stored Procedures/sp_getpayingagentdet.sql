CREATE Procedure [dbo].[sp_getpayingagentdet]                       
@intpaid varchar(10)                       
as begin            
            
SELECT     td_PayingAgent.*, tm_Payams.intPayamid AS Expr1, tm_Counties.intCountieid, tm_States.intStateid            
FROM         td_PayingAgent INNER JOIN            
                      tm_Payams ON td_PayingAgent.intPayamid = tm_Payams.intPayamid INNER JOIN            
                      tm_Counties ON tm_Payams.intCountieid = tm_Counties.intCountieid INNER JOIN            
                      tm_States ON tm_Counties.intStateid = tm_States.intStateid where td_PayingAgent.intpaid like @intpaid    
                          
                          
                          
                       select * from tbl_Users where User_code like @intpaid 
					   --and User_type like 'TST'               
                       
                       select * from dbo.tbl_Attachements where type like 'TST Photo' and intTypeId like @intpaid  
            
end   
