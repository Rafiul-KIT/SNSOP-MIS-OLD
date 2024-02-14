CREATE Procedure [dbo].[getTSTSearch]      
(      
@intStateid varchar(50),      
@intCountieid varchar(50),      
@intPayamid varchar(50),        
@TstName varchar(50)      
)      
as begin      
      
SELECT     tm_States.StateName, tm_Counties.CountieName, tm_Payams.PayamName, td_TstTeamDet.TstName, td_TstTeamDet.intTST      
FROM         tm_States INNER JOIN      
                      tm_Counties ON tm_States.intStateid = tm_Counties.intStateid INNER JOIN      
                      tm_Payams ON tm_Counties.intCountieid = tm_Payams.intCountieid INNER JOIN      
                      td_TstTeamDet ON tm_Payams.intPayamid = td_TstTeamDet.intPayamid      
                       where tm_States.intStateid like @intStateid and tm_Counties.intCountieid like @intCountieid      
                       and tm_Payams.intPayamid like @intPayamid and td_TstTeamDet.TstName like '%'+@TstName +'%'      
                            
                            
end




