CREATE Procedure [dbo].[getPayamsbyID]          
(          
@intPayamid varchar(10)          
)          
as begin          
          
SELECT     tm_Payams.*, tm_Counties.intCountieid AS Expr1, tm_Counties.intCountieid,tm_Counties.CountieName, tm_States.intStateid, tm_States.StateName   
FROM         tm_Payams INNER JOIN    
                         
                      tm_Counties ON tm_Payams.intCountieid = tm_Counties.intCountieid INNER JOIN    
                      tm_States ON tm_Counties.intStateid = tm_States.intStateid where tm_Payams.intPayamid like @intPayamid  
          
end




