CREATE Procedure [dbo].[getPayamsSearch]      
(      
@intStateid varchar(50),  
@intCountieid varchar(50),      
@PayamName varchar(50)      
)      
as begin      
      
SELECT      tm_States.StateName,tm_States.intStateid,tm_Counties.CountieName,tm_Counties.intCountieid,tm_Payams.PayamName,tm_Payams.intPayamid     
FROM         tm_States INNER JOIN    
                      tm_Counties ON tm_States.intStateid = tm_Counties.intStateid INNER JOIN      
                      tm_Payams ON tm_Counties.intCountieid = tm_Payams.intCountieid      
                       where tm_States.intStateid like @intStateid and  tm_Counties.intCountieid like @intCountieid and  tm_Payams.PayamName like '%'+@PayamName +'%'      
                            
                            
end




