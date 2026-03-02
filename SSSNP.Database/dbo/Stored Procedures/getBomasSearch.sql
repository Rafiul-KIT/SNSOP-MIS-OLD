CREATE Procedure [dbo].[getBomasSearch]  
(  
@intStateid varchar(50),  
@intCountieid varchar(50),  
@intPayamid varchar(50),    
@BomaName varchar(50)  
)  
as begin  
  
SELECT     tm_States.StateName, tm_Counties.CountieName, tm_Payams.PayamName, tm_Bomas.BomaName, tm_Bomas.intBomasid ,tm_Bomas.BomaCode 
FROM         tm_States INNER JOIN  
                      tm_Counties ON tm_States.intStateid = tm_Counties.intStateid INNER JOIN  
                      tm_Payams ON tm_Counties.intCountieid = tm_Payams.intCountieid INNER JOIN  
                      tm_Bomas ON tm_Payams.intPayamid = tm_Bomas.intPayamid  
                       where tm_States.intStateid like @intStateid and tm_Counties.intCountieid like @intCountieid  
                       and tm_Payams.intPayamid like @intPayamid and tm_Bomas.BomaName like '%'+@BomaName +'%'  
                        
                        
end




