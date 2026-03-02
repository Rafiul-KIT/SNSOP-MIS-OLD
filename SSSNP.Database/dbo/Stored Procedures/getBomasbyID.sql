--created by siva as on 30-11-2015  
--Purpose : to get Bomas by id  
CREATE Procedure [dbo].[getBomasbyID]  
(  
@intBomasid varchar(10)  
)  
as begin  
  
SELECT     tm_Bomas.*, tm_Payams.intPayamid AS Expr1, tm_Counties.intCountieid, tm_States.intStateid  
FROM         tm_Bomas INNER JOIN  
                      tm_Payams ON tm_Bomas.intPayamid = tm_Payams.intPayamid INNER JOIN  
                      tm_Counties ON tm_Payams.intCountieid = tm_Counties.intCountieid INNER JOIN  
                      tm_States ON tm_Counties.intStateid = tm_States.intStateid where tm_Bomas.intBomasid like @intBomasid  
  
end




